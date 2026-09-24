using System.ComponentModel;
using Structt;

namespace Assignments
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var c = new Classs();
            var loc = c.Location;
            loc.Id = 5;
            c.Location = loc;
        }
    }
}