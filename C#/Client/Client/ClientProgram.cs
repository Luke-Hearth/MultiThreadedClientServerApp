using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    class ClientProgram
    {

        public static void Main(string[] args)
        {
            Console.WriteLine("Started");

            try{
                Client client = new Client();
                Console.WriteLine("Logged in succsessfull");

                while (true)
                {
                    try
                    {

                        string line = client.reader.ReadLine();
                        string[] lineParts = line.Split(' ');

                        switch (lineParts[0])
                        {
                            case "players":
                                client.updatePlayersList(line);
                                break;
                            default:
                                break;
                        }

                        

                        client.updatePlayersList(client.reader.ReadLine());
                        client.updateNewPlayer();
                        client.UpdatePlayerWithBall();
                        client.HasBall();
                        if (ClientInfo.newPlayerID != null)
                        {
                            if (ClientInfo.firstTime)
                            {
                                updateWhoYouAre();
                                ClientInfo.firstTime = false;
                            }
                        }

                        client.threadInit();
                        client.PassBall(ClientInfo.passPlayer);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine(e.StackTrace);
                    }
                }
            }
        catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        static void updateWhoYouAre()
        {
            ClientInfo.thisplayerNumber = ClientInfo.newPlayerID;
        }

       

    }

}
