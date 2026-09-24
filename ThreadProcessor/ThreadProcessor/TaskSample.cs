using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadProcessor
{
    internal class TaskSample
    {
        public async Task SampleMethodAsync(Image image)
        {
            await Connect();
            Task<string> taskA=ReadTableA();
            Task<string> taskB = ReadTableB();
            Task<string> taskC = ReadTableC();
            await Task.WhenAll(taskA, taskB, taskC);

            await Close();
        }
        public async Task Connect()
        {
            Console.WriteLine("Connecting...");
            await Task.Delay(2000);
        }
        public async Task<string> ReadTableA()
        {
            Console.WriteLine("Reading table A");
            await Task.Delay(2000);
            return "I am from Table A";
        }
        public async Task<string> ReadTableB()
        {
            Console.WriteLine("Reading table A");
            await Task.Delay(2000);
            return "I am from Table B";
        }
        public async Task<string> ReadTableC()
        {
            Console.WriteLine("Reading table A");
            await Task.Delay(2000);
            return "I am from Table C";
        }
        public async Task Close()
        {
            await Task.Delay(2000);
        }
        private static Image Process(Image image, int id)
        {
            Console.WriteLine($"{id}. Processing - {image.Id}");
            image.startTime = DateTime.Now;
            Thread.Sleep(3000);
            image.endTime = DateTime.Now;
            Console.WriteLine($"{id}. Processed - {image.Id}");
            return image;
        }
    }
}
