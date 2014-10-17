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

        public Object message_awailable = new Object();


        public EventHandler<MessageReceivedEventArgs> HandleProcessMessage { get; set; }

        public MessageQueue ( )
        {
            incomingMessages = new Queue<IMessage>();
        }


        public void MessageReceived(object sender, MessageReceivedEventArgs arg)
        {
            lock (sync_message)
            {
                incomingMessages.Enqueue(arg.Message);
                Monitor.PulseAll(message_awailable);
            }
        }


        public void Start()
        {
           
            isReady = true;
            Task.Run(() => ProcessMessage());
        }

        public void ProcessMessage()
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
                        Monitor.Wait(message_awailable);
                    }
                }
            }
        }

        private void OnProcessMessage(IMessage message)
        {
            var handler = this.HandleProcessMessage;
            if ( handler!= null)
            {
                HandleProcessMessage(this, new MessageReceivedEventArgs(message));
            }
        }

        public void Stop()
        {

        }

    }
}
