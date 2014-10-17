using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.Message;

namespace ChatServer
{
    public abstract class MessageChannel : IChannel
    {
        protected readonly Queue<IMessage> incomingMessage = null;

        public MessageChannel(string channelName)
        {
            incomingMessage = new Queue<IMessage>();
        }

     


        public void IncomingMessageReceived(object sender, MessageReceivedEventArgs arg)
        {
            incomingMessage.Enqueue(arg.Message);
        }

    }
}
