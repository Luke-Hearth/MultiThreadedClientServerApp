using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Server
    {

        private const int portNum = 8888;

        public static void Main(String[] args)
        {
            RunServer();
        }

        static void RunServer()
        {

            TcpListener listener = new TcpListener(IPAddress.Loopback, portNum);
            listener.Start();


            Console.WriteLine("Awaiting Connection..");
            while (true)
            {
                TcpClient tcpClient = listener.AcceptTcpClient();
                ClientHandler client = new ClientHandler(tcpClient);
                new Thread(new ThreadStart(client.run)).Start();

            }

        }

    }



}

