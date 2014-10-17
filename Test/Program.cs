using System;
using System.Linq;
using CommonLib.Message;

using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            TestMessage message = new TestMessage();
            message.Start();
            message.HandleProcessMessage += ProcessMessage;

            message.MessageReceived(null, new MessageReceivedEventArgs(new Message()));

            Console.ReadKey();

            message.MessageReceived(null, new MessageReceivedEventArgs(new Message()));

            Console.ReadKey();
        }


        public static void ProcessMessage(object sender, MessageReceivedEventArgs arg)
        {
            Console.WriteLine("Processing message");
        }

    }
}
