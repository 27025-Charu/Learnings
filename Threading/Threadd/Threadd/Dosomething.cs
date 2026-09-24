using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threadd
{
    internal class Dosomething
    {
        public async Task DosomethingAsync()
        {
            Console.WriteLine($"Current thread before sleep:{Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(1000);
            Console.WriteLine($"Current thread after sleep:{Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
