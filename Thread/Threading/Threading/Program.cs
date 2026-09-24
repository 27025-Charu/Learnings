namespace Threading
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ThreadTest test=new ThreadTest();
            test.Start();
            Console.WriteLine("Press enter key to stop the thread...");
            Console.ReadLine();
            test.Stop();
            Console.ReadKey();
        }
    }
}
