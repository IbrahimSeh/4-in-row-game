using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfFourInRowServer
{
    public interface IFourInRowCallback
    {
        [OperationContract(IsOneWay = true)]
        void UpdateClientsList(IEnumerable<string> users);

        [OperationContract(IsOneWay = false)]
        bool SendGameRequestToClient(string fromClient);

        [OperationContract(IsOneWay = true)]
        void NewStepInTheGame(double colNum);

        [OperationContract(IsOneWay =true)]
        void UpdateGameOver(string WinnerName);

        [OperationContract(IsOneWay = true)]
        void UpdateInformationsList(string[] info,string[] gamesInfo);

        [OperationContract(IsOneWay = true)]
        void ViewSearchResult(string[] searchResult);
    }
}