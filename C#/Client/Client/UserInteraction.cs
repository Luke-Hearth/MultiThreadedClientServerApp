using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class UserInteraction
    {
        static bool printed = false;
        public static void Run()
        {
            while (true) {
                playerInteraction();
            }
            
        }




        public static void playerInteraction()
        {
            if (ClientInfo.hasBall)
            {
                printed = false;
                Console.WriteLine("You have the ball");
                Console.WriteLine("Press 1 to view game status and 2 to send the ball");
                string input = Console.ReadLine();
                if (input == "1") {
                    Console.WriteLine("New Player: " + ClientInfo.newPlayerID);
                    Console.WriteLine("Player with ball: " + ClientInfo.playerWithBall);
                    Console.WriteLine("Players in the game: " + printplayers());
                }
                else if (input == "2") {
                    Console.WriteLine("Select one of these players to send the ball to: ");
                    Console.WriteLine("Players in the game: " + printplayers());
                    string passPlayerNumb = Console.ReadLine();
                    if (ClientInfo.playerAsString.Keys.Contains(passPlayerNumb)) {
                        ClientInfo.passPlayer = int.Parse(passPlayerNumb);
                        ClientInfo.pass = true;
                    }
                    else{

                        Console.WriteLine(passPlayerNumb + " is not a Valid player number");
                    }
                }
                else {
                    Console.WriteLine("Please enter a character taht is either 1 or 2");
                }
            }
            else
            {
                if (!printed)
                {
                    Console.WriteLine("New Player: " + ClientInfo.newPlayerID);
                    Console.WriteLine("Player with ball: " + ClientInfo.playerWithBall);
                    Console.WriteLine("Players in the game: " + printplayers());
                    Console.WriteLine("Awaiting ball to be passed to you...");
                    printed = true;
                }
            }
        }

         static string printplayers()
        {

            String playersList = "";
            foreach (String player in ClientInfo.playerAsString.Keys)
            {
                playersList += player;
                playersList += " ";

            }

            return playersList;
        }


    }


   
}
