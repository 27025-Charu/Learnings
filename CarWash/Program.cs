namespace CarWash
{
    internal class Program
    {
        static void Main(string[] args)
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 50;
            timer.Elapsed += OnTimerElapsed;
            timer.Start();
            Foo();
            timer.Stop();
            Console.ReadLine();
        }

        private static void Foo()
        {
            for (int i = 0; i < 2000; ++i)
            {
                Console.WriteLine(i);
            }
        }

        private static void OnTimerElapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            PrintTime();
        }

        private static void PrintTime()
        {
            Console.Clear();
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss"));
        }
    }
}
