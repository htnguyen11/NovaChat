using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.Message;

namespace ChatServer.Messaging
{
    public class MessageRouter : IRouter
    {
        private Dictionary<string, IChannel> channels = null;

        public MessageRouter()
        {
            channels = new Dictionary<string, IChannel>();
        }

        public IChannel Route(string routingKey)
        {

            IChannel channel;
            if ( !channels.TryGetValue(routingKey, out channel))
            {
                // log invalid channel or channel does not exists
                throw new NotImplementedException("Implementation of channel to handle invalid channel not yet implemented.");
            }

            return channel;
          
        }


    }
}
