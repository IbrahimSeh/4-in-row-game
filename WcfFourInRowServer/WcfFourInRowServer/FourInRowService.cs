using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WcfFourInRowServer
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
     ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class Four_in_rowService : IFour_in_rowService
    {
        private SortedDictionary<string, IFourInRowCallback> clients;
        private SortedDictionary<string, int> currentGames;
        private SortedDictionary<string, string> opponentPlayers;
        static four_in_rowDB_amaniIbrahimEntities ctx = new four_in_rowDB_amaniIbrahimEntities();


        public Four_in_rowService()
        {
            clients = new SortedDictionary<string, IFourInRowCallback>();
            currentGames = new SortedDictionary<string, int>();
            opponentPlayers = new SortedDictionary<string, string>();
        }

         public void Connect(string clientId, string password)
         {

            // try
            // {
                 Player userVar = (from user in ctx.Players
                                where (user.PlayerId == clientId)
                                select user).FirstOrDefault<Player>();

                 if (userVar != null)
                 {
                     if (userVar.Password == password)
                     {
                    if (clients.ContainsKey(clientId))
                    {
                        UserAlreadyConnectedFault fault = new UserAlreadyConnectedFault()
                        {
                            Message = "UserId: " + clientId + " alraedy connected!!"
                        };
                        throw new FaultException<UserAlreadyConnectedFault>(fault);
                    }
                    
                         IFourInRowCallback callback =
                                       OperationContext.Current.GetCallbackChannel<IFourInRowCallback>();
                         clients.Add(clientId, callback);
                         Thread updateThread = new Thread(UpdateClientsLists);
                         updateThread.Start();
                     }
                     else
                     {
                         UserPasswordFault passwordFault = new UserPasswordFault()
                         {
                             Message = "Error: the password isn't correct, try again!."
                         };
                         throw new FaultException<UserPasswordFault>(passwordFault);
                     }
                 }
                 else
                 {
                     UserIdNotExistFault fault = new UserIdNotExistFault()
                     {
                         Message = "Error: check your user Id ,The user Id \"" + clientId + "\" doesn't exist"
                     };
                     throw new FaultException<UserIdNotExistFault>(fault);
                 }
         }



        private void UpdateClientsLists()
        {
            foreach (var callback in clients.Values)
            {
                callback.UpdateClientsList(clients.Keys);
            }
        }

        public void UpdateInformationList(string fromClient, string choosenClient)
        {
            Player varPlayer = (from player in ctx.Players
                             where player.PlayerId == choosenClient
                             select player).SingleOrDefault();

            int wins = varPlayer.Points;
            int numGames = varPlayer.Games.Count;
            int losesVar = numGames - wins;
            string[] arr = new string[6];
            arr[0] = "Player Id: " + choosenClient;
            arr[1] = "Points : " + wins;
            arr[2] = "Number of games: " + numGames;
            arr[3] = "Number of wins: " + wins;
            int loses = losesVar;
            arr[4] = "Number of loses: " + loses;

            double percent = 0;
            if (numGames != 0) {
                percent = (wins / numGames) * 100;
            }
            arr[5] = "Percent of victories: " + String.Format("{0:0.00}", percent) + "%";

            string[] gamesArr = new string[numGames];
            int index = 0;

            foreach(var g in varPlayer.Games)
            {
                string rivalPlayer = (from game in ctx.Games
                                      where game.GameId == g.GameId
                                      from p in game.Players
                                      where p.PlayerId != choosenClient
                                      select p.PlayerId).SingleOrDefault();

                gamesArr[index] = "\nGame Id: " + g.GameId + "\nPlayers: " + choosenClient + " & " + rivalPlayer +
                                  "\nWinner: " + g.WinnerName;
                index++;
            }


            Thread t2 = new Thread(() => clients[fromClient].UpdateInformationsList(arr,gamesArr));
            t2.Start();
            return;
        }


        public void Register(string clientId, string clientName, string password)
        {
            try
            {
                Player newPlayer = new Player();
                newPlayer.PlayerId = clientId;
                newPlayer.Name = clientId;
                newPlayer.Password = password;

                ctx.Players.Add(newPlayer);
                ctx.SaveChanges();
            }
            catch (Exception)
            {
                UserIdExistsFault fault = new UserIdExistsFault()
                {
                    Message = "Error: User Id \"" + clientId + "\" already exists, please choose another one."

                };
                throw new FaultException<UserIdExistsFault>(fault);
            }

        }

        public bool SendGameRequest(string fromClient, string toClient)
        {
            bool answer = false;
            if (clients.ContainsKey(toClient))
            {
                answer = clients[toClient].SendGameRequestToClient(fromClient);
            }

            if (answer == true)
            {
                opponentPlayers.Add(fromClient, toClient);
                opponentPlayers.Add(toClient, fromClient);

                Player player1 = (from user in ctx.Players
                                  where (user.PlayerId == fromClient)
                                  select user).FirstOrDefault<Player>();

                Player player2 = (from user in ctx.Players
                                  where (user.PlayerId == toClient)
                                  select user).FirstOrDefault<Player>();

                Game newGame = new Game()
                {
                    WinnerPoints=0,
                    TimeOfBeginning = DateTime.Now,
                    TimeOfEnd = DateTime.Now,
                };
                ctx.Games.Add(newGame);
                ctx.SaveChanges();

                Game gameVar = (from user in ctx.Games
                                where (user.GameId == newGame.GameId)
                                select user).FirstOrDefault<Game>();
                player1.Games.Add(gameVar);
                ctx.Entry(player1).State = System.Data.Entity.EntityState.Modified;
                player2.Games.Add(gameVar);
                ctx.Entry(player1).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();

                currentGames.Add(fromClient, newGame.GameId);
                currentGames.Add(toClient, newGame.GameId);
            }
            return answer;

        }


        public void Disconnect(string userName)
        {
            clients.Remove(userName);
            Thread updateThread = new Thread(UpdateClientsLists);
            updateThread.Start();
        }

        public bool AddStepToTheGame(double colNum, string fromClient)
        {
            Thread t2 = new Thread(() => clients[opponentPlayers[fromClient]].NewStepInTheGame(colNum));
            t2.Start();
            return false;
        }

        public void GameOver(string WinnerName)
        {
            int gameId = currentGames[WinnerName];
            Game gameVar = (from game in ctx.Games
                            where game.GameId == gameId
                            select game).SingleOrDefault();

            gameVar.WinnerName = WinnerName;
            gameVar.TimeOfEnd = DateTime.Now;
            ctx.Entry(gameVar).State = System.Data.Entity.EntityState.Modified;

            Player playerVar = (from p in ctx.Players
                                where p.PlayerId == WinnerName
                                select p).SingleOrDefault();
            playerVar.Points++;
            ctx.Entry(playerVar).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();

            Thread t2 = new Thread(() => clients[opponentPlayers[WinnerName]].UpdateGameOver(WinnerName));
            t2.Start();
        }

        public void SearchPlayers(string fromClient, string orderBy)
        {                     

               var result = (from t in ctx.Players
                             where t.Games.Count!=0
                          select new
                          {
                              name = t.PlayerId,
                              wins = t.Points,
                              game = t.Games.Count,
                              loses = t.Games.Count() - t.Points,
                              victoriesPercent = (t.Points / t.Games.Count)
                          }).ToList();

                if(orderBy=="Name") result = result.OrderBy(x => x.name).ToList();
                else if(orderBy== "GamesNumber") result = result.OrderBy(x => x.game).ToList();
                else if(orderBy== "VictoriesNumber") result = result.OrderBy(x => x.wins).ToList();
                else if(orderBy== "LossesNumber") result = result.OrderBy(x => x.loses).ToList();
                else if(orderBy== "PercentOfVictories") result = result.OrderBy(x => x.victoriesPercent).ToList();
                else if(orderBy == "Points") result = result.OrderBy(x => x.wins).ToList();

                string[] arr = new string[result.Count];
                int index = 0;
                foreach(var p in result)
                {
                    arr[index] = p.name;
                    index++;
                }

                Thread th = new Thread(() => clients[fromClient].ViewSearchResult(arr));
                th.Start();                               
            
        }

        public void closeTheBoard(string clientName)
        {
            opponentPlayers.Remove(clientName);
            currentGames.Remove(clientName);
            
        }

        public void GetAllGames(string clientName, string certainGames)
        {
            if (certainGames == "Finish")
            {
                var GamesVar = from game in ctx.Games
                               where game.TimeOfBeginning != game.TimeOfEnd
                               select new
                               {
                                   players = game.Players,
                                   Winner = game.WinnerName,
                                   date = game.TimeOfBeginning
                               };

                string[] info = new string[GamesVar.Count()];
                string[] players = new string[2];
                int index = 0;
                int playersIndex = 0;


                foreach (var g in GamesVar)
                {
                    foreach (var p in g.players)
                    {
                        players[playersIndex++] = p.PlayerId;
                    }
                    info[index++] = "Players: " + players[0] + " & " + players[1] +
                                  "\nWinner: " + g.Winner +
                                  "\nDate: " + g.date;
                    playersIndex = 0;
                }

                Thread th = new Thread(() => clients[clientName].ViewSearchResult(info));
                th.Start();
            }

            else if(certainGames == "Current")
            {
                var CurrGames = from game in ctx.Games
                                where game.TimeOfBeginning == game.TimeOfEnd
                                select new
                                {
                                    players = game.Players,
                                    Winner = game.WinnerName,
                                    date = game.TimeOfBeginning
                                };

                string[] info = new string[CurrGames.Count()];
                string[] players = new string[2];
                int index = 0;
                int playersIndex = 0;


                foreach (var g in CurrGames)
                {
                    foreach (var p in g.players)
                    {
                        players[playersIndex++] = p.PlayerId;
                    }
                    info[index] = "Players: " + players[0] + " & " + players[1] +
                                  "\nWinner: " + g.Winner +
                                  "\nDate: " + g.date;
                    playersIndex = 0;
                }

                Thread th = new Thread(() => clients[clientName].ViewSearchResult(info));
                th.Start();
            }
        }
    }

}
