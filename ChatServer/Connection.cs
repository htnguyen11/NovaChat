using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatServer.Messaging;
using CommonLib;
using CommonLib.Message;

namespace ChatServer
{
    public class Connection : IConnection
    {
        private ICommunicator communicator = null;

        private string id = String.Empty;

        public Connection (ICommunicator communicator)
        {
            if (communicator == null)
                throw new ArgumentNullException("communicator");

            this.communicator = communicator;

            Guid guid = Guid.NewGuid();

            id = guid.ToString();
        }

        public EventHandler<MessageReceivedEventArgs> MessageHandler { get; set; }

        public void Start()
        {
            communicator.MessageReceivedHandler += HandleMessageReceived;

            communicator.Start();
        }

        void IConnection.Send(CommonLib.Message.IMessage message)
        {
            if (message == null)
                throw new ArgumentNullException("message");

            communicator.Send(message);
        }

        void Messaging.ISubscriber.Subscribe(Messaging.ISubscription subscription)
        {
            if (subscription == null)
                throw new ArgumentNullException("subscription");

            subscription.Subscribe(this);
        }

        public void HandleMessageReceived(object sender, MessageReceivedEventArgs arg)
        {
            var handler = MessageHandler;
            if ( handler != null )
            {
                handler(this, arg);
            }
        }

        public string ID
        {
            get
            {
                return this.id;
            }
        }

    }
}
