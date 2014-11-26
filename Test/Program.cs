using System;
using System.Linq;
using CommonLib.Message;

using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using CommonLib;
using ChatServer;

namespace Test
{
    class Program
    {


        static void Main(string[] args)
        {
            IConnectionListener listener = new ConnectionListener("127.0.0.1", 9000);
            listener.IncomingConnectionHandler += Connection_Connected;
            listener.Start();

            
            
            Console.ReadKey();

        }

        public static  void Connection_Connected(object ssender, IncomingConnectionEventArgs arg)
        {
            Console.WriteLine("connected");
        }



    }
}
