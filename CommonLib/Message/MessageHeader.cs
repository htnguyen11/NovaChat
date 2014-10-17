using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Message
{
    public class MessageHeader
    {


        

        /// <summary>
        /// Message ID at the time of creation.
        /// </summary>
        string MessageID { get; set; }


        /// <summary>
        /// Correllation ID to match Reply message with a corresponding Response message ID.
        /// </summary>
        string CorrelationID { get; set; }


        /// <summary>
        /// Source of the message.  Represent the sender of this message.
        /// </summary>
        string Source { get; set; }


        /// <summary>
        /// Final destination of this message.
        /// </summary>
        string Destionation { get; set; }

        /// <summary>
        /// Date and time of message created.
        /// </summary>
        DateTime Time { get; set; }
    }
}
