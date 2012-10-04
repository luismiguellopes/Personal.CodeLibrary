using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Personal.Lib.Threading;
using System.Threading;

namespace Personal.Lib.Exchanger.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.ExchangerTest();
        }

        static readonly int numberOfThreads = 4;

        void ExchangerTest()
        {
            Console.WriteLine("Start testing");
            Thread[] threads = new Thread[numberOfThreads];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(TestThread);
                threads[i].Name = string.Format("{0}", i);
                threads[i].Start();
            }
            Console.WriteLine("press enter to stop");
            Console.ReadLine();
            stop = true;
            Console.WriteLine("stopping...");
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }
            Console.WriteLine("The End!");
            Console.ReadLine();
        }

        static Exchanger<string> exc = new Exchanger<string>();
        static volatile bool stop = false;

        void TestThread()
        {
            int count = 0;
            string threadName = Thread.CurrentThread.Name;
            do
            {
                string result = exc.Exchange(threadName);
                if (result.Equals(threadName))
                {
                    Console.WriteLine("ERROR: same name returned!");
                    break;
                }
                else
                {
                    ++count;
                    Console.Write(" [{0}<->{1}]", threadName, result);
                }
                Thread.Sleep(0);
            } while (!stop);
            Console.WriteLine("{0} values switched in thread {1}", count, threadName);
        }
    }
}
