using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threading
{
    internal class ThreadTest
    {
        private static readonly List<string> myList = new List<string> { "Apple", "Banana", "Cherry", "Date" };
        private bool isRunning = false;
        public void Start()
        {
            Thread t1 = new Thread(Run);
            Thread t2 = new Thread(Run);
            isRunning = true;
            t1.Start();
            t1.Sleep(1000);

            t2.Start();
        }
        public void Stop()
        {
        }
        private void Run()
        {
            Guid threadInfo = new Guid();
            Console.WriteLine($"{threadInfo} : Starting thread");
            while (isRunning || intList.Count == 0)
            {
                Thread.Sleep(1000);
                Console.WriteLine(threadInfo + " " + DateTime.Now.ToString());
            }

            Console.WriteLine($"{threadInfo} : Exiting thread");
        }
        public void PrintThreadState()
        {
            Console.WriteLine($"Thread state 1: {t1.Threadstate()}");

            Console.WriteLine($"Thread state 1: {t2.Threadstate()}");

        }
    }
}
