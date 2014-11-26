using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.Message;

namespace CommonLib
{
    public interface ICommunicator :ISubscriber
    {
        /// <summary>
        /// This handler is used to raise event to handle received message from the communication channel.
        /// </summary>
        EventHandler<MessageReceivedEventArgs> MessageReceivedHandler { get; set; }


        /// <summary>
        /// Send message to the communication channel.
        /// </summary>
        /// <param name="message"></param>
        void Send(IMessage message);


        /// <summary>
        /// Start communicator by start accepting incoming message.
        /// </summary>
        void Start();


        /// <summary>
        /// Stop Communicator from accepting incoming message.
        /// </summary>
        void Stop();

    }
}
