using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugging
{
    abstract class Color
    {
        string colorName;
        protected Color() 
        { 
            Console.WriteLine("Color is created");
        }
        protected Color(string colour)
        {
            colorName = colour;
        }
    }
}
