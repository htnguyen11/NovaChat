using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.Message;
using CommonLib;

namespace ChatServer
{
    public class PubSubChannel
    {

        private Dictionary<string, ISubscribable<ICommunicator>> subscriptions = null;

        private string channelName = String.Empty;

        public PubSubChannel(string channelName)
        {
            this.channelName = channelName;

            subscriptions = new Dictionary<string, ISubscribable<ICommunicator>>();
        }


        public void AddSubscription(string topic, ISubscribable<ICommunicator> subscription)
        {
            lock (subscriptions)
            {
                if (subscription == null)
                    throw new ArgumentNullException("subscription");

                if (String.IsNullOrEmpty(topic))
                    throw new Exception("Failed to subscribe a topic.  Reason: toppic is empty or null.");

                if ( !subscriptions.ContainsKey(topic))
                {
                    

                    subscriptions.Add(topic, subscription);
                }
            }
        }

        public void RemoveSubscription(string topic)
        {
            lock(subscriptions)
            {
                if ( subscriptions.ContainsKey(topic))
                {
                    var subscription = subscriptions[topic];

                    subscriptions.Remove(topic);
                }
            }
        }

        public void AddSubscriber(string topic, ICommunicator communicator)
        {
            lock (subscriptions)
            {
                if (subscriptions.ContainsKey(topic))
                {
                    ISubscribable<ICommunicator> subscription = subscriptions[topic];

                    subscription.AddSubscriber(communicator);
                }
            }
        }

        public void Publish(IMessage message)
        {
            lock(subscriptions)
            {
                MessageType type = message.Type;
                string topic = Enum.GetName(typeof(MessageType), type);

                if ( subscriptions.ContainsKey(topic))
                {
                   ISubscribable<ICommunicator> subscription = subscriptions[topic];

                   subscription.SendMessage(message);
                }
            }
        }
    }
}
