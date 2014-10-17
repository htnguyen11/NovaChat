using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib;

namespace ChatServer
{

    public class IncomingConnectionEventArgs : EventArgs
    {
        public ICommunicationChannel Channel { get; private set; }

        public IncomingConnectionEventArgs(ICommunicationChannel channel)
        {
            Channel = channel;
        }
    }
}
