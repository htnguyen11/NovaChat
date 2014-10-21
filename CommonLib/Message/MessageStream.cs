using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

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


        public async Task Send (IMessage message)
        {
            byte[] message_data = GetBytes<IMessage>(message);

            await SendAsync(message_data);
        }

        private async Task SendAsync(byte[] message_data)
        {
            await nStream.WriteAsync(message_data, 0, message_data.Length);
        }

        private byte[] GetBytes<T>(T message)
        {
            byte[] serializedMessage = SerializeMessage<T>(message);
            int len = serializedMessage.Length;

            byte[] messageSize  = BitConverter.GetBytes(len);

            byte[] message_data = new byte[len+4];
            if ( messageSize.Length > 4)
            {
                throw new Exception("Invalid message size.");
            }

            Array.Copy(messageSize,message_data, messageSize.Length);
            Array.Copy(serializedMessage,0,message_data,4,serializedMessage.Length);

            return message_data;
        }


        private byte[] SerializeMessage<T> (T message)
        {
            using (MemoryStream mstream = new MemoryStream())
            {
                try
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(mstream, message);
                    return mstream.ToArray();
                }
                catch ( SerializationException ex)
                {
                    ///write to logger
                    ///to be implemtned
                    string ex_message = ex.Message;
                }
                throw new Exception("Failed to serialize message.");
            }
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
