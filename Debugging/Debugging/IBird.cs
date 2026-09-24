using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugging
{
    internal interface IBird
    {
        public void Fly()
        {
            Console.WriteLine("Bird is flying");
        }
    }
}
