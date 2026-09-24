using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugging
{
    internal class Sample
    {
        List<int> l = new List<int>() { 1, 3, 4, 5, 6, 6, 7, 8, 9, 10 };
        public int localvariable = 0;
        private Crow crow = new Crow();

        public void Add(int a)
        {
            l.Add(11);
        }

        public int SumOfIntegers(List<int> l)
        {
            int sum = 0;
            foreach (var item in l)
            {
                sum += item;
            }
            return sum;
        }
        public int sumThroughLinq(List<int> l)
        {
            return l.Sum();
        }

        internal void Increment(int x)
        {
            x++;
            Console.WriteLine($"1. Value of x inside Increment method: {x}");
            DoubleIncrement(ref x);
        }
        public void DoubleIncrement(ref int x)
        {
            x++;
            x++;
            AnotherIncrement(x);
            Console.WriteLine($"2. Value of x inside DoubleIncrement method: {x}");
        }

        public void AnotherIncrement(int a)
        {
            a++;
            Console.WriteLine($"3. Value of a inside AnotherIncrement method: {a}");
        }
        public void IncrementLocalVariable(out int var)
        {
            var = 10;
        }

        public void MakeCrowFly()
        {
            crow.Fly();
        }
    }
}
