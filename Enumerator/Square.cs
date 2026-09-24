using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enumerator
{
    internal class Square : IShape
    {
        public double size { get; set; }
        public string Name => "Square";

        public string Description => throw new NotImplementedException();

        public double CalculateArea()
        {
            return size * size;
        }

        public void PrintText()
        {
            Console.WriteLine($"Length : {size} ,  Area: {CalculateArea()}")
        }
    }
}
