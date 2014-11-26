using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CommonLib.Message
{
    public class MessageQueue
    {
        private readonly Queue<IMessage> incomingMessages = null;
        private bool isReady;

        public Object sync_message = new Object();


        public EventHandler<MessageReceivedEventArgs> HandleProcessMessage { get; set; }

        public MessageQueue ( )
        {
            incomingMessages = new Queue<IMessage>();
        }

        /// <summary>
        /// Rasied event when message is received from communication channel via communicator.  
        /// Add message to queue to be ready for processing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        public void QueueMessage(IMessage message)
        {
            lock (sync_message)
            {
                incomingMessages.Enqueue(message);

                if (incomingMessages.Count > 0)
                {
                    Monitor.PulseAll(sync_message);
                }
               
            }
        }


        public void Start()
        {
           
            isReady = true;
            Task.Run(() => ProcessMessage());
        }

        private void ProcessMessage()
        {
            while (isReady)
            {
                lock (sync_message)
                {
                    IMessage message = null;

                    if ( incomingMessages.Count > 0 )
                    {
                        message = incomingMessages.Dequeue();
                        OnProcessMessage(message);
                    }
                    else
                    {

                        Monitor.Wait(sync_message);

                    }
                }
            }
        }

        private void OnProcessMessage(IMessage message)
        {
            var handler = this.HandleProcessMessage;
            if ( handler!= null)
            {
                MessageType type = message.Type;
                
                HandleProcessMessage(this, new MessageReceivedEventArgs(message));
            }
        }

        public void Stop()
        {
            lock (sync_message)
            {
                isReady = false;
                Monitor.PulseAll(sync_message);
            }
        }

    }
}
