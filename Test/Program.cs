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
        private ConnectionManager manager = new ConnectionManager();

        private IList<IConnection> connections = new List<IConnection>();

       
        static void Main(string[] args)
        {

            Run(null, 0);
        }

        public static void Run(MessageQueue mqueue, int timeout)
        {
            var contextwork = new Context();
            Callback(state =>
                {
                    var context = (Context)state;
                    

                },
                contextwork
            );

            IMessage message = new Message();
            mqueue.QueueMessage(message);
            
        }


        public static void Callback (WaitCallback callback , object state )
        {
            Console.ReadKey();
        }
        public void Connection_Connected(object ssender, IncomingConnectionEventArgs arg)
        {

            ICommunicator communicator = new Communicator(arg.Channel);
            IConnection connection = new Connection(communicator);
            communicator.MessageReceivedHandler += MessageReceivedHandler;
            //connection.MessageHandler += MessageReceivedHandler;
            connections.Add(connection);

            connection.Start();
        }


        public static  void MessageReceivedHandler(object sender, MessageReceivedEventArgs arg)
        {
            IMessage message = arg.Message;
            Console.WriteLine(message.Source);
            Thread.Sleep(3000);
        }


    }

    public class Context
    {

    }
}
