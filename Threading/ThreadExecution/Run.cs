namespace ThreadExecution
{
    internal class Run
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Main method started.");

            // Running the async method inside a background Task
            await Task.Run(async () => await RunOperationsAsync());

            Console.WriteLine("Main method completed.");
        }

        // The async method containing 3 distinct await operations
        static async Task RunOperationsAsync()
        {
            // 1. First await operation
            await ExecuteFirstPrintAsync();

            // 2. Second await operation
            await ExecuteSecondPrintAsync();

            // 3. Third await operation
            await ExecuteThirdPrintAsync();
        }

        // Methods returning Task to make them awaitable, each doing a WriteLine
        static Task ExecuteFirstPrintAsync()
        {
            Console.WriteLine("Executing: Operation 1 printed.");
            return Task.CompletedTask; // Returns a completed task to satisfy the Task return type
        }

        static Task ExecuteSecondPrintAsync()
        {
            Console.WriteLine("Executing: Operation 2 printed.");
            return Task.CompletedTask;
        }

        static Task ExecuteThirdPrintAsync()
        {
            Console.WriteLine("Executing: Operation 3 printed.");
            return Task.CompletedTask;
        }
    }
}
