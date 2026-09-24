using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Reflection;

namespace ThreadProcessor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            ImageProcessor imageProcessor = new ImageProcessor();
            imageProcessor.Start(5);
            while (true)
            {
                Console.WriteLine("Enter number of images to add:");
                ConsoleKey keyPressed = Console.ReadKey().Key;
                if (keyPressed == ConsoleKey.Spacebar)
                {
                    for (int i = 0; i < 10; ++i)
                    {
                        Image image = new Image() { Id = count++ };
                        imageProcessor.Add(image);
                    }
                }

                if (keyPressed == ConsoleKey.Enter)
                {
                    break;
                }
            }
            imageProcessor.Stop();
            Console.ReadKey();
        }
    }
}
