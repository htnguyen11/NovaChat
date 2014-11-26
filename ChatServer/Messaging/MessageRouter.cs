using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.Message;

namespace ChatServer.Messaging
{
    public class MessageRouter
    {
        private Dictionary<string, IChannel> channels = null;

        public MessageRouter()
        {
            channels = new Dictionary<string, IChannel>();
        }

        public void Route(IMessage message)
        {
            string routingKey = message.RoutingKey;


            if (channels.ContainsKey(routingKey))
            {
                IChannel channel = channels[routingKey];
                
            }
        }


    }
}
