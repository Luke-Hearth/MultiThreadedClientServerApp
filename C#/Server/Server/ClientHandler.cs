using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class ClientHandler
    {

       
        int thisPlayerNum;

        private object param;

        public ClientHandler(object param)
        {

            this.param = param;
        }


        public void run()
        {
            TcpClient tcpClient = (TcpClient) param;
            using (Stream stream = tcpClient.GetStream())
            {

                {
                    //Creates scanner and writed so that the server can send and read messages from the client
                    StreamReader scanner = new StreamReader(stream);
                    StreamWriter writer = new StreamWriter(stream);
                    try
                    {

                        thisPlayerNum = AddInfo(thisPlayerNum);

                        writer.WriteLine("Success");
                        MessageHandler messageHandler = new MessageHandler(scanner , writer , thisPlayerNum);
                        new Thread(new ThreadStart(messageHandler.Run)).Start();
                        
                        while (true)
                        { 
                            SendPlayers(writer);
                            SendNewPlayer(writer);
                            SendPlayerWithBall(writer);

                            //If this player has the ball allow it to send mesages about passing ball

                            hasBall(writer, thisPlayerNum);

                            //if player has ball take some commands otherwise do nothing
                        }

                    }
                    catch (Exception e)
                    {


                    }
                }

            }
        }



        void SendPlayers(StreamWriter writer)
        {

            String playersList = "players ";
            foreach (String player in GameInfo.players.Keys)
            {
                playersList += player;
                playersList += " ";

            }
            writer.WriteLine(playersList);

        }

        void SendPlayerWithBall(StreamWriter writer)
        {

            String playersWithBaLL = "playerwithball ";

            foreach (int key in GameInfo.gameStatus.Keys)
            {

                bool hasBall;
                GameInfo.gameStatus.TryGetValue(key, out hasBall);
                if (hasBall)
                {
                    playersWithBaLL += key.ToString();
                    writer.WriteLine(playersWithBaLL);
                }

            }


        }

        void SendNewPlayer(StreamWriter writer)
        {

            String newPlayerInfo = "newplayer " + GameInfo.newPLayerID;
            writer.WriteLine(newPlayerInfo);
        }

        int AddInfo(int thisPlayerNum)
        {
            //Assings a new unique ID for each player incrementally
            thisPlayerNum = GameInfo.playerNumber;
            GameInfo.newPLayerID = (thisPlayerNum.ToString());
            GameInfo.players.TryAdd((thisPlayerNum.ToString()) , false);
            GameInfo.playerNumber += 1;
            Console.WriteLine("New connection with player: " + thisPlayerNum);
            Console.WriteLine("All players: " + printplayers());

            //if there are no player teh first player to connect gets the ball

            if (GameInfo.gameStatus.IsEmpty)
            {
                GameInfo.gameStatus.TryAdd(thisPlayerNum, true);
            }
            else
            {
                GameInfo.gameStatus.TryAdd(thisPlayerNum, false);
            }
            return thisPlayerNum;
        }



        void hasBall(StreamWriter writer, int thisPlayerNum)
        {
            bool hasBall;
            GameInfo.gameStatus.TryGetValue(thisPlayerNum, out hasBall);
            if (hasBall)
            {
                writer.WriteLine("Ball");

            }
            else
            {
                writer.WriteLine("NoBall");
            }
        }


        string printplayers()
        {

            String playersList = "";
            foreach (String player in GameInfo.players.Keys)
            {
                playersList += player;
                playersList += " ";

            }

            return playersList;
        }





    }
}
