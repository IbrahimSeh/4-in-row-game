using FourInRowClient.FourInRowServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FourInRowClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Four_in_rowServiceClient Client { get; set; }
        public ClientCallback Callback { get; set; }
        public string Username { get; set; }
        public Game4inRow board;
        public SearchWindow searchWindow;

        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Callback.displayGameRequest += DisplayGameRequest;
            Callback.updateUsers += UpdateTheUsers;
            Callback.newStepInTheGame += UpdateNewStepInTheGame;
            //board.newStepInTheGame += updateNewStep;
            //board.gotARow += IGotARow;
            //board.closeGame += CloseGame;
            //searchWindow.searchplayers += SearchPlayers;
            Callback.updateGameOver += UpdateGameOver;
            Callback.updateGamesInfo += UpdatePlayerGamesList;
            Callback.viewSearchResult += ViewSearchResult;

        }

        private void CloseGame()
        {
            Client.closeTheBoard(Username);
        }

        private void SearchPlayers(string sortedBy)
        {
            Thread t = new Thread(()=> Client.SearchPlayers(Username,sortedBy));
            t.Start();
        }

        private void ViewSearchResult(string[] searchResult)
        {
            searchWindow.DisplaySearchResults(searchResult);
        }


        private void UpdateGameOver(string winnerName)
        {
            board.DispalyTheWinner(winnerName);
        }

        private void IGotARow()
        {
            Client.GameOver(Username);
        }

        private void UpdatePlayerGamesList(string[] info, string[] gamesInfo)
        {
            lbInfoList.ItemsSource = info;
            lbGamesList.ItemsSource = gamesInfo;
        }

        private void UpdateNewStepInTheGame(double colNum)
        {
            board.updateBallInTheBoard(colNum);

        }

        private void UpdateTheUsers(string[] users)
        {
            users = users.Where(w => w != Username).ToArray();
            lbUsers.ItemsSource = users;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            try
            {
                Client.Disconnect(Username);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                System.Environment.Exit(System.Environment.ExitCode);
            }
        }

        private void updateNewStep(double colNum)
        {
            Client.AddStepToTheGame(colNum, Username);
        }

        
        private void SendGameRequest_Click(object sender, RoutedEventArgs e)
        {
            bool answer = false;
            if (lbUsers.SelectedItem != null)
            {
                answer = Client.SendGameRequest(Username,
                       lbUsers.SelectedItem as string);

                if (answer == true)
                {
                    MessageBox.Show(lbUsers.SelectedItem as string + " accepted your request");
                    board = new Game4inRow();
                    board.newStepInTheGame += updateNewStep;
                    board.gotARow += IGotARow;
                    board.closeGame += CloseGame;
                    board.my_turn = true;
                    board.Title = Username;
                    board.Show();
                }
                else MessageBox.Show(lbUsers.SelectedItem as string + " declined your request");
            }

            else
            {
                MessageBox.Show("please choose a player from the list of connected clients.");
            }


        }

        private bool DisplayGameRequest(string fromClient)
        {
            MessageBoxResult res = MessageBoxResult.None;
            while (res == MessageBoxResult.None)
            {
                res = (MessageBox.Show(fromClient + " sent request to play with you, do you accept?", "Game resquest",
                                 MessageBoxButton.YesNo));
                if (res == MessageBoxResult.Yes)
                {
                    
                    board = new Game4inRow();
                    board.my_turn = false;
                    board.newStepInTheGame += updateNewStep;
                    board.gotARow += IGotARow;
                    board.closeGame += CloseGame;
                    board.Title = Username;
                    board.Show();
                    return true;
                }
                if (res == MessageBoxResult.No) return false;
            }
            return false;
        }

        private void SellectedItem_Changed(object sender, SelectionChangedEventArgs e)
        {
            Client.UpdateInformationList(Username, lbUsers.SelectedItem as string);
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            searchWindow = new SearchWindow();
            searchWindow.searchplayers += SearchPlayers;
            searchWindow.getAllGames += SearchAllGames;
            searchWindow.Show();
        }

        private void SearchAllGames(string certainGame)
        {
            Thread t = new Thread(() => Client.GetAllGames(Username, certainGame));
            t.Start();
        }
    }
}
