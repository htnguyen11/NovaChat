using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.Message;
using CommonLib;

namespace ChatServer
{
    public class Server : IServer
    {
        /// <summary>
        /// This IP address used to start to server.  All clients will connect to this ip.
        /// </summary>
        private string ipAddress;

        /// <summary>
        /// This is the port to listen for incoming connections.
        /// </summary>
        private int port;


        /// <summary>
        /// this is set to true after all the components required to run the server is initialized.
        /// </summary>
        private bool isReady;

        private IConnectionListener connectionListener = null;

        private CommunicationMananger communicationManager = null;

        public Server()
        {
            Initialize();
        }

        public Server(string ipAddress, int port)
        {
            this.ipAddress = ipAddress;
            this.port = port;
            Initialize();
        }


        /// <summary>
        /// Initialize components to start server.
        /// </summary>
        private void Initialize()
        {
            //Read in server ipaddress and port to accept incoming connection.
            //to be implemented
            
            this.connectionListener = new ConnectionListener(ipAddress, port);
            connectionListener.IncomingConnectionHandler += HandleIncomingConnection;

            communicationManager = new CommunicationMananger();
            


            isReady = true;
        }

        void IServer.Start()
        {
            if ( isReady )
            {
                //Start accepting incoming tcp connection.
                connectionListener.Start();
            }
        }

        void IServer.Close()
        {
            connectionListener.Close();
            isReady = false;
        }


        private void HandleIncomingConnection(object sender, IncomingConnectionEventArgs arg)
        {
            ICommunicator communicator = new Communicator(arg.Channel);

            communicationManager.CommunicatorConnected(communicator);
        }
    }
}
