using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib;
using CommonLib.Message;

namespace ChatServer
{
    public class Subscription<TSubscriber> : ISubscribable<TSubscriber> where TSubscriber : ICommunicator
    {
        private List<TSubscriber> subscribers = null;
        private int subscriberCount = 0;

        public Subscription()
        {
            subscribers = new List<TSubscriber>();
        }
        

        /// <summary>
        /// Add subscriber to this subscription.
        /// </summary>
        /// <param name="subscriber"></param>
        public void AddSubscriber(TSubscriber subscriber)
        {
            lock (subscribers)
            {
                if (!subscribers.Contains(subscriber))
                {
                    subscribers.Add(subscriber);
                    subscriberCount = subscribers.Count;
                }
            }
        }


        /// <summary>
        /// Remove a subscriber from this subscription.
        /// </summary>
        /// <param name="subscriber"></param>
        public void RemoveSubscriber(TSubscriber subscriber)
        {
            lock ( subscribers)
            {
                if ( subscribers.Contains(subscriber))
                {
                    subscribers.Remove(subscriber);
                    subscriberCount = subscribers.Count;
                }
            }
        }

        public void SendMessage(IMessage message)
        {
            for ( int i = 0; i < subscriberCount;i++)
            {

                ICommunicator communicator = subscribers[i];
                communicator.Send(message);
            }
        }

        public void ClearAll()
        {
            lock(subscribers)
            {
                subscribers.Clear();
            }
        }
    }
}
