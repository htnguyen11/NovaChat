using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ChatServer.Messaging
{
    public class SubscriptionMananger
    {

        private Dictionary<string, ISubscription> subscriptions = null;
        private ReaderWriterLockSlim subscriptionManangerLock = new ReaderWriterLockSlim();

        public SubscriptionMananger ()
        {
            subscriptions = new Dictionary<string, ISubscription>();
        }


        public void AddSubscription(ISubscription subscription)
        {
            subscriptionManangerLock.EnterWriteLock();

            if (!subscriptions.ContainsKey(subscription.ID))
            {
                subscriptions.Remove(subscription.ID);
            }

            subscriptionManangerLock.ExitWriteLock();
        }


        public void RemoveSubscription(ISubscription subscription)
        {
            subscriptionManangerLock.EnterWriteLock();
            if ( subscriptions.ContainsKey(subscription.ID))
            {
                subscriptions.Remove(subscription.ID);
            }
            subscriptionManangerLock.ExitWriteLock();
        }

        public ISubscription GetSubscriptionByKey(string key)
        {
            subscriptionManangerLock.EnterReadLock();
            ISubscription subscription = null;

            if ( subscriptions.ContainsKey(key))
            {
                subscription = subscriptions[key];
            }

            subscriptionManangerLock.ExitReadLock();

            return subscription;
        }

    }
}
