namespace ThreadExecution
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Current main thread:{Thread.CurrentThread.ManagedThreadId}");
            Thread t1 = new Thread(() => Console.WriteLine($"Thread 1: {Thread.CurrentThread.ManagedThreadId}"));
            Thread t2 = new Thread(() => Console.WriteLine($"Thread 2: {Thread.CurrentThread.ManagedThreadId}"));
            Thread t3 = new Thread(PrintMinus);
            Thread t4 = new Thread(PrintPlus);
            string result = "";
            var thread = new Thread(() => result = "hello");
            thread.Start();
            thread.Join();
            Console.WriteLine(result);
            t1.Start();
            t3.Start("hello");
            t2.Start();
            t4.Start("hello");
            Console.WriteLine("Main thread completed execution");
            Console.ReadKey();
        }
        private static void PrintPlus(object obj)
        {
            for (int i = 0; i < 100; i++)
            {
                Console.Write("+");
            }
        }

        private static void PrintMinus(object obj)
        {
            for (int i = 0; i < 100; i++)
            {
                Console.Write("-");
            }
        }
    }
}
