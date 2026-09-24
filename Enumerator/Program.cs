using System.Drawing;
using System.Globalization;

namespace Enumerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Rectangle rect=new Rectangle();
            rect.Length = 10;
            rect.Width = 5;
            rect.Description = "This is a basic rectangle";
        }
        IShape shape = rect;
        shape.Description="Edit";
        Console.WriteLine(shape.Description);
        List<Ishape>  shapes=new List<IShape<>()>();
    }
}
