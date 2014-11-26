using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.Message;

namespace CommonLib
{
    /// <summary>
    /// This class is used as a communicator for client and server to communicate.
    /// </summary>
    public class Communicator : ICommunicator
    {

        private ICommunicationChannel communicationChannel = null;

        private string id = String.Empty;

        public Communicator(ICommunicationChannel communicationChannel)
        {
            if (communicationChannel == null)
                throw new ArgumentNullException("channel");

            this.communicationChannel = communicationChannel;

            Guid guid = Guid.NewGuid();
            id = guid.ToString();
        }


        /// <summary>
        /// This handler is used to raise event to handle received message from the communication channel.
        /// </summary>
        public EventHandler<MessageReceivedEventArgs> MessageReceivedHandler { get; set; }


        void ISubscriber.Receive(IMessage message)
        {
            communicationChannel.Send(message);
        }

        /// <summary>
        /// Send message to the communication channel.
        /// </summary>
        /// <param name="message"></param>
        void ICommunicator.Send(Message.IMessage message)
        {
            
            communicationChannel.Send(message);
        }

        /// <summary>
        /// Start communicator by start accepting incoming message.
        /// </summary>
        void ICommunicator.Start()
        {
            communicationChannel.MessageReceivedHandler += MessageReceived;

            communicationChannel.Open();
        }


        /// <summary>
        /// Stop Communicator from accepting incoming message.
        /// </summary>
        void ICommunicator.Stop()
        {
            communicationChannel.Close();
        }

        private void MessageReceived(object sender, MessageReceivedEventArgs arg )
        {
            var handler = MessageReceivedHandler;
            if ( handler != null )
            {
                handler(this, arg);
            }
        }

        string ISubscriber.ID
        {
            get
            {
               return id;
            }
        }
    }
}
