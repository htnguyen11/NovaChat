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

        private ICommunicationChannel channel = null;
        static void Main(string[] args)
        {
            /*System.Net.IPAddress address = System.Net.IPAddress.Parse("127.0.0.1");
            TcpListener listener = new TcpListener(address, 9000);
            listener.Start();

            TcpClient client = listener.AcceptTcpClient();

            MessageStream mstream = new MessageStream(client.GetStream());*/
            
            Program pr = new Program();

            //pr.Receive(mstream);

           IConnectionListener connectionListener = new ConnectionListener("127.0.0.1", 9000);
            
            connectionListener.IncomingConnectionHandler += pr.Connection_Connected;

            connectionListener.Start();

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


        public void MessageReceivedHandler(object sender, MessageReceivedEventArgs arg)
        {
            IMessage message = arg.Message;
            Console.WriteLine(message.Source);
        }


    }
}
