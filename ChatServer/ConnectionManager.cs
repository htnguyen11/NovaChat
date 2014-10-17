using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib;
using CommonLib.Message;

namespace ChatServer
{
    public class ConnectionManager : ICommunicationController
    {
        //Dication to hold a collection of communicators(client connection) connected to this server.
        private Dictionary<string, ICommunicator> communicators = null;

        public ConnectionManager()
        {

        }

        private void Initialize()
        {
            communicators = new Dictionary<string, ICommunicator>();
        }


        private void IncomingConnectionConnected(object sender, IncomingConnectionEventArgs arg)
        {
            ICommunicator communicator = new Communicator(arg.Channel);

            communicator.MessageReceivedHandler += MessageReceivedHandler;
        }

        private void MessageReceivedHandler(object sender, MessageReceivedEventArgs arg)
        {
            IMessage messge = arg.Message;
        }
    }
}
