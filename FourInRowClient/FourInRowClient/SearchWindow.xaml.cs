using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FourInRowClient
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        public SearchWindow()
        {
            InitializeComponent();
        }


        private void rb_ViewAllSubscribers_checked(object sender, RoutedEventArgs e)
        {
            comboBox.IsEnabled = true;
            //searchplayers((comboBox.SelectedItem as ComboBoxItem).Content.ToString());
            searchplayers("Name");


        }

        public delegate void AllGamesDelegate(string certainGame);
        public event AllGamesDelegate getAllGames;

        private void rb_FinishedGames_checked(object sender, RoutedEventArgs e)
        {
            comboBox.IsEnabled = false;
            getAllGames("Finish");

        }

        private void rb_CurrentGames_checked(object sender, RoutedEventArgs e)
        {
            comboBox.IsEnabled = false;
            getAllGames("Current");
        }

        public delegate void searchPlayersDelegate(string sortedBy);
        public event searchPlayersDelegate searchplayers;

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            searchplayers((comboBox.SelectedItem as ComboBoxItem).Content.ToString());
            
        }



        public void DisplaySearchResults(string[] results)
        {
            SearchResultBox.ItemsSource = results;
        }
    }
}
