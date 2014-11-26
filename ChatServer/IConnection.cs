using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.Message;

namespace ChatServer
{
    public interface IConnection : Messaging.ISubscriber
    {
        void Send(IMessage message);

        EventHandler<MessageReceivedEventArgs> MessageHandler { get; set; }

        void Start();

        string ID { get; }
    }
}
