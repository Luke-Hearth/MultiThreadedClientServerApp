using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    class Client
    {

        const int port = 8888;
        public StreamReader reader;
        public StreamWriter writer;
        public Client()
        {
            TcpClient tcpClient = new TcpClient("localhost", port);
            NetworkStream stream = tcpClient.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);

            string line = reader.ReadLine();

            if (line.Trim().ToLower() != "success") throw new Exception(line);
        }
        public void UpdatePlayerWithBall()
        {
            string line = reader.ReadLine();
            string[] words = line.Split(' ');
            if (words[0].Trim().ToLower() == ("playerwithball"))
            {

                ClientInfo.playerWithBall = words[1];
            }


        }

        public void HasBall()
        { 
            string line = reader.ReadLine();

            if (line.Trim().ToLower() == ("ball"))
            {
                ClientInfo.hasBall = true;
            }
            else if (line.Trim().ToLower() == ("noball"))
            {
                ClientInfo.hasBall = false;
            }
            else
            {
                return;
            }
        }



        public void updatePlayersList(string line)
        {

            string[] words = line.Split(' ');
            //Checks to see if this is the right command else returns
            if (words[0].Trim().ToLower() == ("players"))
            {
                ClientInfo.playerAsString.Clear();
                for (int i = 1; i < words.Length; i++)
                {
                    if (i != words.Length - 1)
                    {
                        ClientInfo.playerAsString.TryAdd(words[i] , false);
                            
                    }
                }
            }
        }

        public void updateNewPlayer()
        {
            string line = reader.ReadLine();
            string[] words = line.Split(' ');
            if (words[0].Trim().ToLower() == ("newplayer"))
            {
                ClientInfo.newPlayerID = words[1];
            }
        }


        public void PassBall(int playernumber)
        {
            if (ClientInfo.pass)
            { 
                writer.WriteLine("pass " + playernumber);
                writer.Flush();
                ClientInfo.pass = false;
                ClientInfo.hasBall = false;
            }

        }


        public void threadInit()
        {

            if (ClientInfo.firstTime)
            {
                new Thread(new ThreadStart(UserInteraction.Run)).Start();
                ClientInfo.firstTime = false;
            }

        }



    }
}


