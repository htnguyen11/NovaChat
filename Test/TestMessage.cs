using System;
using System.Linq;

using CommonLib.Message;

namespace Test
{
    public class TestMessage : IMessage
    {
        byte[] IMessage.Body
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        string IMessage.CorrelationID
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        string IMessage.MessageID
        {
            get { throw new NotImplementedException(); }
        }

        MessageType IMessage.Type
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
