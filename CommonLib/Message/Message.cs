using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Message
{
    [Serializable]
    public class Message : IMessage
    {
        private string messageID = String.Empty;


        public Message()
        {
            Guid guid = Guid.NewGuid();
            messageID = guid.ToString();
        }


        string IMessage.MessageID
        {
            get
            {
                return this.messageID;
            }
        }

        byte[] IMessage.Body { get; set; }


        /// <summary>
        /// Get or Set the type of this message.
        /// 
        /// Possible Type:
        /// 
        /// Text
        /// Command
        /// 
        /// 
        /// </summary>
        MessageType IMessage.Type { get; set; }


        /// <summary>
        /// Correlation ID to for reply message.
        /// </summary>
        string IMessage.CorrelationID { get; set; }
    }
}
