using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.Message;

namespace ChatServer.Messaging
{
    public class GroupMessageHandler : IMessageHandler
    {
        private IRouter router = null;


        public GroupMessageHandler(IRouter router)
        {
            if (router == null)
                throw new ArgumentNullException("router");

            this.router = router;
        }

        public void Handle(IMessage message)
        {
            IChannel channel = router.Route(message.RoutingKey);


            if (channel != null)
                Route(channel, message);
        }

        private void Route(IChannel channel, IMessage message)
        {
            
        }
    }
}
