namespace Threadd
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Main thread started");
            Console.WriteLine($"Thread currently running:{Thread.CurrentThread.ManagedThreadId}");
            Dosomething obj = new Dosomething();
            Task task = obj.DosomethingAsync();
            await task;
            Thread.Sleep(1000);
            Console.WriteLine($"Main thread after await task: {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
