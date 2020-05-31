using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class MessageHandler
    {

        private StreamReader scanner;
        private StreamWriter writer;
        private int thisPlayerNum;



        public MessageHandler(StreamReader scanner, StreamWriter writer, int thisPlayerNum)
        {
            this.scanner = scanner;
            this.thisPlayerNum = thisPlayerNum;
            this.writer = writer;
        }


    public void Run()
        {
            try
            {
                while (true)
                {
                    //Checks if it even needs to get the new line
                    String line = scanner.ReadLine();
                    String[] words = line.Split(' ');
                    Console.WriteLine(line);
                    //Checks if the input is actually what we want i.e the "pass" keyword
                    if (words[0].Trim().CompareTo("pass") == 0)
                    {
                        int passPlayerNum = int.Parse(words[1]);
                        //Check if the playernumber is a different one than the client
                        if (thisPlayerNum != passPlayerNum)
                        {
                            if (GameInfo.gameStatus.ContainsKey(passPlayerNum))
                            {
                                GameInfo.gameStatus.TryUpdate(thisPlayerNum, false , true);
                                GameInfo.gameStatus.TryUpdate(passPlayerNum, true, false);
                                Console.WriteLine("The ball has been passed to player " + passPlayerNum);
                            }
                            else
                            {

                                Console.WriteLine("The ball doesnt pass the player has disconnected " + thisPlayerNum + " still has ball");
                            }
                        }
                        else
                        {

                            Console.WriteLine("The ball is passed to: " + thisPlayerNum);

                        }

                    }


                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Player " + thisPlayerNum + " has disconnected");
                RemoveInfo(thisPlayerNum);
                scanner.Close();
                writer.Close();
            }
        }

        void RemoveInfo(int thisPlayerNum)
        {
            bool tryremove;
            GameInfo.players.TryRemove(thisPlayerNum.ToString() , out tryremove);
            //If the dissconnected guy has the ball
            if (GameInfo.gameStatus.ContainsKey(thisPlayerNum))
            {
                bool tryremoveagain;
                GameInfo.gameStatus.TryRemove(thisPlayerNum , out tryremoveagain);
                //If the game is not empty
                if (!GameInfo.gameStatus.IsEmpty)
                {
                    //Set an element in the map to have the ball
                    foreach (int key in GameInfo.gameStatus.Keys)
                    {
                        GameInfo.gameStatus.TryUpdate(key, true , false);
                        Console.WriteLine("The ball has been passed to " + key + " due to disconnect");
                        break;
                    }

                }
            }
            else
            {

                bool tryremoveagain;
                GameInfo.gameStatus.TryRemove(thisPlayerNum, out tryremoveagain);
            }

            Console.WriteLine("All players: " + printplayers());

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
