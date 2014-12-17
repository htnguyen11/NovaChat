using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.Message;

namespace ChatServer.Messaging
{
    public interface IMessageHandler
    {
        void Handle(IMessage message);
    }
}
