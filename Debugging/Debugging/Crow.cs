using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugging
{
    internal class Crow : Color,IBird
    {
        public Crow()
        {
            Console.WriteLine("Crow is created");
        }
        public void Fly()
        {
            Console.WriteLine("Crow is flying");
        }
    }
}
