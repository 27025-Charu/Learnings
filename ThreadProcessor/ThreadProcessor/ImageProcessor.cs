using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadProcessor
{
    internal class ImageProcessor
    {
        private int _threadCounter = 0;
        private List<Image> _images = new List<Image>();
        private List<Thread> _threads = new List<Thread>();
        private AutoResetEvent _threadEvent = new AutoResetEvent(true);
        public bool IsRunning { get; private set; }
        public bool Start(int threadCount)
        {
            if (IsRunning) return true;
            IsRunning = true;
            for (int i = 0; i < threadCount; i++)
            {
                Thread thread = new Thread(Run);
                _threads.Add(new Thread(Run));
                thread.Start();
            }
            return true;
        }
        public bool Stop()
        {
            IsRunning = false;
            _threads.Clear();
            _threadEvent.Set();
            return true;
        }
        public void Add(Image image)
        {
            lock (_images)
            {
                _images.Add(image);
                _threadEvent.Set();
            }
        }
        private static bool Process(Image image, int id)
        {
            Console.WriteLine($"{id}. Processing - {image.Id}");
            image.startTime = DateTime.Now;
            Thread.Sleep(3000);
            image.endTime = DateTime.Now;
            Console.WriteLine($"{id}. Processed - {image.Id}");
            return true;
        }
        private void Run()
        {
            int threadId = Interlocked.Increment(ref _threadCounter);
            while (IsRunning || _images.Count > 0)
            {
                Image image = null;
                bool hasImage = false;
                lock (_images)
                {
                    if (_images.Count > 0)
                    {
                        hasImage = true;
                        image = _images[0];
                        _images.RemoveAt(0);
                        Console.WriteLine($"{threadId}. Found image to process-{image.Id}");
                    }
                    else
                    {
                        hasImage = false;
                    }
                }
                if (!hasImage && IsRunning)
                {
                    Console.WriteLine($"{threadId}. No image is found for processing");
                    _threadEvent.WaitOne(5000);
                }
                if (image != null)
                {
                    Process(image, threadId);
                }
            }
            Console.WriteLine($"{threadId} is stopped.");
        }
    }
}