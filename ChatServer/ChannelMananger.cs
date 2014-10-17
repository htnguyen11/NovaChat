using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public class ChannelMananger
    {
        Dictionary<string, IChannel> channels = null;

        public ChannelMananger()
        {
            channels = new Dictionary<string, IChannel>();
        }

        public void AddChannel(string name, IChannel channel)
        {
            lock ( channels)
            {
                if ( !channels.ContainsKey(name))
                {
                    channels.Add(name, channel);
                }
            }
        }

        public void RemoveChannel(string name)
        {
            lock(channels)
            {
                if ( channels.ContainsKey(name))
                {
                    IChannel channel = channels[name];
                    
                    
                }
            }
        }
    }
}
