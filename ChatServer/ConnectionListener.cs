using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;
using CommonLib;

namespace ChatServer
{
    public class ConnectionListener : IConnectionListener
    {
        private string ipAddress;
        private int port;
        private TcpListener listener = null;

        public ConnectionListener(string ipAddress, int port)
        {
            this.ipAddress = ipAddress;
            this.port = port;
        }

        public EventHandler<IncomingConnectionEventArgs> IncomingConnectionHandler { get; set; }


        /// <summary>
        /// Start listening for connection on a seperate thread.
        /// </summary>
        void IConnectionListener.Start()
        {
            Task.Run(() => AcceptConnection());
        }

        private async Task AcceptConnection()
        {
            TcpClient client = await listener.AcceptTcpClientAsync();

            ClientConnected(client);
        }

        private void ClientConnected(TcpClient client)
        {
            Task.Run(() => OnIncomingConnection(client));
        }


        private void OnIncomingConnection(TcpClient client)
        {
            var handler = IncomingConnectionHandler;
            if ( handler != null )
            {
                handler(this, new IncomingConnectionEventArgs(new CommunicationChannel(client)));
            }
        }

        void IConnectionListener.Close()
        {
            throw new NotImplementedException();
        }

        EventHandler<IncomingConnectionEventArgs> IConnectionListener.IncomingConnecctionHandler
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
