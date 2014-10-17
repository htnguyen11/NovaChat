using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace CommonLib.Message
{
    internal class MessageStream
    {
        /// <summary>
        /// object instance used to do network IO.
        /// </summary>
        private NetworkStream nStream = null;

        public MessageStream (NetworkStream stream)
        {

            if (stream == null)
                throw new ArgumentNullException("stream");

            nStream = stream;
        }


        public void Send (IMessage message)
        {

        }


        /// <summary>
        /// Asynchronously receive incoming message without blocking the caller.
        /// </summary>
        /// <returns></returns>
        public async Task<IMessage> ReceiveAsync()
        {
            IMessage message = null;

            byte[] messageData = await ReceiveMessage();

            return message;
        }

        private async Task<byte[]> ReceiveMessage()
        {
            //read first 4 bytes to determine size of the incoming message

            byte[] prefix = new byte[4];

            int byteCount = await nStream.ReadAsync(prefix, 0, prefix.Length);

            //if less than 4 bytes were read read until we get all 4 bytes
            if ( byteCount < 4)
            {
                byteCount += await nStream.ReadAsync(prefix, byteCount, prefix.Length - byteCount);
            }

            int messageSize = BitConverter.ToInt32(prefix, 0);


            //Create new byte array to store the actual message data
            byte[] messageData = new byte[messageSize];

            int dataLeft = messageSize;

            int totalByteRecv = 0;

            while ( totalByteRecv < messageSize)
            {
                byteCount = await nStream.ReadAsync(messageData, totalByteRecv, dataLeft);

                totalByteRecv += byteCount;
                dataLeft -= byteCount;
            }

            return messageData;
        }
    }
}
