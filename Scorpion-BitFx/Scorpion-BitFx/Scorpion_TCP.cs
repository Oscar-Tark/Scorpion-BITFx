﻿using System;
using System.Net;
using SimpleTCP;

namespace ScorpionBitFx
{
    public class Scorpion_TCP
    {
        SimpleTcpServer tcp = new SimpleTcpServer();
        SimpleTcpClient tcp_cl = new SimpleTcpClient();
        Scorpion do_on;

        public Scorpion_TCP(int port, Scorpion fm1)
        {
            //Alaways uses non localhost IP
            do_on = fm1;
            Console.WriteLine("Server is running: {0}", start_server(ref port));
            return;
        }

        public bool start_server(ref int port)
        {
            tcp.Start(port, false);
            tcp.ClientConnected += Tcp_ClientConnected;
            tcp.DataReceived += Tcp_DataReceived;
            return tcp.IsStarted;
        }


        public bool stop_server()
        {
            tcp.Stop();
            return tcp.IsStarted;
        }

        void Tcp_ClientConnected(object sender, System.Net.Sockets.TcpClient e)
        {
            //Executes mongo
            Console.WriteLine("Client Connected {0}");
            return;
        }

        void Tcp_DataReceived(object sender, Message e)
        {
            IPEndPoint ipep = (IPEndPoint)e.TcpClient.Client.RemoteEndPoint;
            IPAddress ipa = ipep.Address;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Got string: {0} from {1}", e.MessageString, ipa);
            do_on.execute_command(e.MessageString);
        }
    }
}