using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FourInRowClient.FourInRowServiceReference;

namespace FourInRowClient
{

    public class ClientCallback : IFour_in_rowServiceCallback
    {
        public delegate void UpdateListDelegate(string[] users);
        public event UpdateListDelegate updateUsers;
        public void UpdateClientsList(string[] users)
        {
            updateUsers?.Invoke(users);
        }

        public delegate bool DisplayGameRequestDelegate(string fromClient);
        public event DisplayGameRequestDelegate displayGameRequest;
        public bool SendGameRequestToClient(string fromClient)
        {
            return displayGameRequest(fromClient);
        }

        public delegate void NewStepInTheGameDelegate(double colNum);
        public event NewStepInTheGameDelegate newStepInTheGame;
        public void NewStepInTheGame(double colNum)
        {
            newStepInTheGame(colNum);
        }

        public delegate void UpdateInfoListDelegate(string[] info,string[] gamesInfo);
        public event UpdateInfoListDelegate updateGamesInfo;
        public void UpdateInformationsList(string[] info, string[] gamesInfo)
        {
            updateGamesInfo(info,gamesInfo);
        }


        public delegate void UpdateGameOverDelegate(string winnerName);
        public event UpdateGameOverDelegate updateGameOver;
        public void UpdateGameOver(string winnerName)
        {
            updateGameOver(winnerName);
        }


        public delegate void ViewSearchResultDelegate(string[] searchResult);
        public event ViewSearchResultDelegate viewSearchResult;
        public void ViewSearchResult(string[] searchResult)
        {
            viewSearchResult(searchResult);
        }
    }
}
