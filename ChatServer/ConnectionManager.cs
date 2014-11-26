using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ChatServer.Messaging;
using CommonLib;
using CommonLib.Message;

namespace ChatServer
{
    public class ConnectionManager
    {
        //Dication to hold a collection of communicators(client connection) connected to this server.
        private Dictionary<string, IConnection> connections = null;

        private ReaderWriterLockSlim connectionsLock = null;

        private MessageBroker messageBroker = null;

        public ConnectionManager()
        {
            Initialize();
        }

        private void Initialize()
        {
            connections = new Dictionary<string, IConnection>();
            connectionsLock = new ReaderWriterLockSlim();
            messageBroker = new MessageBroker();
        }


        public void IncomingConnectionConnected(ICommunicator communicator)
        {

            IConnection connection = new Connection(communicator);

            connection.MessageHandler += MessageReceivedHandler;

            connectionsLock.EnterWriteLock();

            connections.Add(connection.ID, connection);

            connectionsLock.ExitWriteLock();

            connection.Start();
            
        }

        private void MessageReceivedHandler(object sender, MessageReceivedEventArgs arg)
        {
            IMessage message = arg.Message;
            messageBroker.ScheduleMessage(message);
        }
    }
}
