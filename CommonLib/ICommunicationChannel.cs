using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.Message;

namespace CommonLib
{
    public interface ICommunicationChannel
    {

        EventHandler<MessageReceivedEventArgs> MessageReceivedHandler { get; set; }

        void Open();

        void Close();

        void Send(IMessage message);
    }
}
