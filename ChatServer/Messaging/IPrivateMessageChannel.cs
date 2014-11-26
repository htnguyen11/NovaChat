using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Messaging
{
    public interface IPrivateMessageChannel : IChannel
    {

        void Subscribe(ISubscription subscription)
        {

        }
    }
}
