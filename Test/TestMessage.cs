using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using CommonLib.Message;

namespace Test
{
    public class TestMessage
    {
        private readonly Queue<IMessage> incomingMessages = null;
        private bool isReady;

        public Object sync_message = new Object();

        public Object message_awailable = new Object();


        public EventHandler<MessageReceivedEventArgs> HandleProcessMessage { get; set; }

        public TestMessage ( )
        {
            incomingMessages = new Queue<IMessage>();
        }

        /// <summary>
        /// Rasied event when message is received from communication channel via communicator.  
        /// Add message to queue to be ready for processing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        public void MessageReceived(object sender, MessageReceivedEventArgs arg)
        {
            lock (sync_message)
            {
                incomingMessages.Enqueue(arg.Message);
                Monitor.PulseAll(sync_message);
               
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
                        Console.WriteLine("message queue empty waiting for messae");
                        Monitor.Wait(sync_message);
                        Console.WriteLine("message received waking up to process message");

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
