using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Message
{

    /// <summary>
    /// All messages sent to server must implement this interface.
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// ID for this message
        /// </summary>
        string MessageID { get; }

        /// <summary>
        /// Body for this message
        /// The body can be serialized object or message string.
        /// </summary>
        byte[] Body { get; set; }

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
        MessageType Type { get; set; }


        /// <summary>
        /// Correlation ID to for reply message.
        /// </summary>
        string CorrelationID { get; set; }



        /// <summary>
        /// Routing key used to route message message channel(s).
        /// </summary>
        string RoutingKey { get; set; }



        string Source { get; set; }

        string Destination { get; set; }
    }
}
