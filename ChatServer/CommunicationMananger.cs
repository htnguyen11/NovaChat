using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.Message;
using CommonLib;
namespace ChatServer
{
    public class CommunicationMananger
    {

        private PubSubChannel pubsubChannel = null;
        private MessageQueue messageQueue = null;


        private Dictionary<string, ICommunicator> communicators = null;

        public CommunicationMananger()
        {
            messageQueue = new MessageQueue();
            communicators = new Dictionary<string, ICommunicator>();
        }

        public void Start()
        {
            messageQueue.HandleProcessMessage += ProcessMessage;
            messageQueue.Start();
        }


        public void Communicator_MessageReceived(object sender, MessageReceivedEventArgs arg)
        {
            messageQueue.MessageReceived(sender,arg);
        }

        /// <summary>
        /// Process messages from the queue.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        private void ProcessMessage(object sender, MessageReceivedEventArgs arg)
        {

        }


        public void CommunicatorConnected(ICommunicator communicator)
        {
            lock ( communicators)
            {
                Guid guid = Guid.NewGuid();

                communicators.Add(guid.ToString(), communicator);

                communicator.MessageReceivedHandler += Communicator_MessageReceived;
                communicator.Start();
            }
        }

    }
}
