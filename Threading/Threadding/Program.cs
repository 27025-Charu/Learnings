namespace Threadding
{
    internal class Program
    {
        //static async Task Main(string[] args)
        //{
        //    Console.WriteLine("Main method started.");

        //    // Running the async method inside a background Task
        //    await Task.Run(async () => await RunOperationsAsync());

        //    Console.WriteLine("Main method completed.");
        //    Console.ReadKey();
        //}

        //// The async method containing 3 distinct await operations
        //static async Task RunOperationsAsync()
        //{
        //    // 1. First await operation
        //    await ExecuteFirstPrintAsync();

        //    // 2. Second await operation
        //    await ExecuteSecondPrintAsync();

        //    // 3. Third await operation
        //    await ExecuteThirdPrintAsync();
        //}

        //// Methods returning Task to make them awaitable, each doing a WriteLine
        //static Task ExecuteFirstPrintAsync()
        //{
        //    Console.WriteLine("Executing: Operation 1 printed.");
        //    return Task.CompletedTask; // Returns a completed task to satisfy the Task return type
        //}

        //static Task ExecuteSecondPrintAsync()
        //{
        //    Console.WriteLine("Executing: Operation 2 printed.");
        //    return Task.CompletedTask;
        //}

        //static Task ExecuteThirdPrintAsync()
        //{
        //    Console.WriteLine("Executing: Operation 3 printed.");
        //    return Task.CompletedTask;
        //}
        static async Task Main(string[] args)
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}-Main method started");
            await DisplayName();
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}-Name is displayed");
            await DisplayAge();
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}-Age is displayed");
            await DisplayPlace();
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}-Place is displayed");
            Console.ReadKey();
        }
        static async Task DisplayName()
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}-Before delay in name display");
            await Task.Delay(1000);
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}-After delay in name display");
            Console.WriteLine($"Arun");
        }
        static async Task DisplayAge()
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}-Before delay in age display");
            await Task.Delay(1000);
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}-After delay in age display");
            Console.WriteLine($"20");
        }
        static async Task DisplayPlace()
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}-Before delay in place display");
            await Task.Delay(1000);
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}-After delay in place display");
            Console.WriteLine("CBE");
        }
    }
}
