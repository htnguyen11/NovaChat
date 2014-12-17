using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ChatServer.Messaging
{
    public class Subscription : ISubscription
    {
        private IList<ISubscriber> subscribers = null;

        private ReaderWriterLockSlim subscriptionLock = null;


        public Subscription(string name)
        {
            subscribers = new List<ISubscriber>();
            subscriptionLock = new ReaderWriterLockSlim();
        }


        public void Subscribe(ISubscriber subscriber)
        {
            
            subscriptionLock.EnterWriteLock();

            if ( !subscribers.Contains(subscriber))
            {
                subscribers.Add(subscriber);
            }

            subscriptionLock.ExitWriteLock();
        }

        public void UnSubscribe(ISubscriber subscriber)
        {
            subscriptionLock.EnterReadLock();

            if ( subscribers.Contains(subscriber))
            {
                subscribers.Remove(subscriber);
            }

            subscriptionLock.ExitReadLock();
        }

        public string Name { get;private set ;}
        public string ID { get; private set; }
    }
}
