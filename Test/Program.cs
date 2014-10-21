using System;
using System.Linq;
using CommonLib.Message;

using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        private bool isRunning;
        private Object sync_message = new Object();
        private Queue<string> queue = new Queue<string>();

        static void Main(string[] args)
        {
            Program pr = new Program();
            Task.Run(() => pr.Test());

            pr.Test_two("Gone wild");
            pr.Test_two("What the hell");

            Thread.Sleep(1000);
            pr.Test_two("What the hell");
            pr.Test_two("What the hell");
            pr.Test_two("What the hell");
            pr.Test_two("What the hell");
            pr.Test_two("What the hell");

            Console.ReadKey();
        }


        public  void Test()
        {
            while ( true )
            {
                lock (sync_message)
                {

                    //Thread.Sleep(10000);
                    if (queue.Count > 0)
                    {
                        string text = queue.Dequeue();
                        Monitor.PulseAll(sync_message);
                        Thread.Sleep(5000);
                        Console.WriteLine(text);
                    }
                    else
                    {
                        Monitor.Wait(sync_message);
                    }
                }
            }
        }

        public void Test_two(string text)
        {
            lock(sync_message)
            {
                Console.WriteLine("enqueu text");
                queue.Enqueue(text);
                Monitor.PulseAll(sync_message);
            }
        }

    }
}
