using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student
{
    abstract class Print
    {
        public virtual void PrintMessage()
        {
            Console.WriteLine("Print class");
        }
    }
    class PrintChild : Print
    {
        public override void PrintMessage()
        {
            Console.WriteLine("Print child class");
        }
    }
}
