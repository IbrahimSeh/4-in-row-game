using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfFourInRowServer
{
    [ServiceContract(CallbackContract = typeof(IFourInRowCallback))]
    public interface IFour_in_rowService
    {
        [OperationContract]
        [FaultContract(typeof(UserPasswordFault))]
        [FaultContract(typeof(UserIdNotExistFault))]
        void Connect(string clientId, string password);

        [OperationContract]
        [FaultContract(typeof(UserIdExistsFault))]
        void Register(string clientId, string clientName, string password);

        [OperationContract]
        void UpdateInformationList(string fromClient, string choosenClient);

        [OperationContract]
        bool SendGameRequest(string fromClient, string toClient);

        [OperationContract]
        void Disconnect(string userName);

        [OperationContract]
        bool AddStepToTheGame(double colNum, string fromClient);

        [OperationContract]
        void GameOver(string WinnerName);

        [OperationContract]
        void SearchPlayers(string fromClient, string orderBy);

        [OperationContract]
        void GetAllGames(string clientName, string certainGames);

        [OperationContract]
        void closeTheBoard(string clientName);


    }
}
