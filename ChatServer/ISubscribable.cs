using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.Message;

namespace ChatServer
{
    public interface ISubscribable<TSubsriber>
    {

        void AddSubscriber(TSubsriber subscriber);
        void RemoveSubscriber(TSubsriber subscriber);
        void SendMessage(IMessage message);
    }
}
