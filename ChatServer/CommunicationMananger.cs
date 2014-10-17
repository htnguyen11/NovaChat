using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.Message;

namespace ChatServer
{
    public class CommunicationMananger
    {

        private PubSubChannel pubsubChannel = null;

        public CommunicationMananger()
        {
            
        }

        public void Start()
        {

        }


        public void Communicator_MessageReceived(object sender, MessageReceivedEventArgs arg)
        {
            MessageType type = arg.Message.Type;
        }


    }
}
