using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enumerator
{
    internal interface IShape
    {
        string Name { get; }
        string Description { get; }
        public double CalculateArea();
        public void PrintText();
    }
}
