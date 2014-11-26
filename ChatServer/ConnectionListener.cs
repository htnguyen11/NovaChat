using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;
using CommonLib;

namespace ChatServer
{

    public enum ListenerState
    {
        READY,
        RUNNING
    };

    public class ConnectionListener : IConnectionListener
    {
        private string ipAddress;
        private int port;
        private TcpListener listener = null;

        private ListenerState state;
        
        public ConnectionListener(string ipAddress, int port)
        {
            this.ipAddress = ipAddress;
            this.port = port;

            if ( port > 8000  && !String.IsNullOrEmpty(ipAddress))
                state = ListenerState.READY;
        }

        public EventHandler<IncomingConnectionEventArgs> IncomingConnectionHandler { get; set; }


        /// <summary>
        /// Start listening for connection on a seperate thread.
        /// </summary>
        void IConnectionListener.Start()
        {
            if (listener != null)
                return;

            if (state == ListenerState.READY)
            {
                IPAddress address = IPAddress.Parse(ipAddress);

                listener = new TcpListener(address, port);

                state = ListenerState.RUNNING;

                listener.Start();

                Task.Run(() => AcceptConnection());
            }
        }

        private async Task AcceptConnection()
        {
            
            while (state == ListenerState.RUNNING)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();

                ClientConnected(client);
            }
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
           
        }
    }
}
