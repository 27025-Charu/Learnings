using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bird
{
    internal interface IBird
    {
        void Fly();
    }
    class Crow : IBird
    {
        public void Fly()
        {
            Console.WriteLine("Crow is flying");
        }
    }
    class Dove : IBird
    {
        public void Fly()
        {
            Console.WriteLine("Dove is flying");
        }
    }
}
