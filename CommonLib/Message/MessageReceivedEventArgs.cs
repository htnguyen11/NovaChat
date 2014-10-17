using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Message
{
    public class MessageReceivedEventArgs : EventArgs
    {

        public IMessage Message { get; private set; }

        public MessageReceivedEventArgs ( IMessage message )
        {
            this.Message = message;
        }
    }
}
