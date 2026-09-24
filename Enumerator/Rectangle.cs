using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enumerator
{
    internal class Rectangle : Shape
    {

        public double Length { get; set; }
        public double Width { get; set; }
        public override string Name => "Rectangle";

        public double CalculateArea()
        {
            return Length * Width;
        }

        public override void PrintText()
        {
            
        }
        List<int> l=new();
    }
}
