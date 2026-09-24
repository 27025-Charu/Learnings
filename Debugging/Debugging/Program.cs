using Microsoft.Win32.SafeHandles;

namespace Debugging
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Sample sam = new Sample();
            sam.Add(10);
            int x = 0;
            sam.Increment(x);
            Console.WriteLine($"Value of x after Increment: {x}");
            int y = 1;
            sam.IncrementLocalVariable(out y);
            Console.WriteLine($"Value of y after IncrementLocalVariable: {y}");
            sam.MakeCrowFly();
        }
    }
}
