using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;
using CommonLib.Message;

namespace CommonLib
{
    public class CommunicationChannel : ICommunicationChannel
    {

        private MessageStream messageStream = null;

        private TcpClient client = null;


        public CommunicationChannel ( TcpClient client )
        {

            if ( client == null )
            {
                throw new ArgumentNullException("client");
            }

            //create new MessageStream to read/write message.
            messageStream = new MessageStream(client.GetStream());

            this.client = client;
        }


        public EventHandler<MessageReceivedEventArgs> MessageReceivedHandler { get; set; }


        /// <summary>
        /// Asynchronously accept incoming message.
        /// </summary>
        /// <returns></returns>
        public async Task AcceptMessage()
        {
            IMessage message = await messageStream.ReceiveAsync();

            OnMessageReceived(message);
           
        }

        private void OnMessageReceived(IMessage message)
        {
            var handler = MessageReceivedHandler;

            if ( handler != null)
            {
                handler(this, new MessageReceivedEventArgs(message));
            }
        }


        /// <summary>
        /// Open channel by accepting incoming message.
        /// </summary>
        void ICommunicationChannel.Open()
        {
            if (!client.Connected)
            {
                throw new Exception("Attempted to open channel without TCP socket connection.");
            }

            Task.Run(() => AcceptMessage());

        }


        /// <summary>
        /// Close channel.  Stop all incoming message from this channel.
        /// </summary>
        void ICommunicationChannel.Close()
        {
            if ( client != null && client.Connected)
            {
                client.Close();
            }
        }

        /// <summary>
        /// Send Message.
        /// </summary>
        /// <param name="message"></param>
        void ICommunicationChannel.Send(IMessage message)
        {
            if (client.Connected)
            {
                messageStream.Send(message);
            }
        }
    }
}
