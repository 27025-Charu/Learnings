using System.ComponentModel;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Addition addition = new Addition();
            addition.Add(3, 4);
            addition.Add(2.3, 5);
            Student s1 = new Student {Id=123,Name="Alice"};
            s1.Id = 12345;
            Console.Write(s1.Id);
            Console.ReadKey();
        }
        class Addition
        {
            public void Add(double x, double y)
            {
                Console.WriteLine(x + y);
            }
            public void Add(int x, int y)
            {
                Console.WriteLine(x + y);
            }
        }
        public struct Student()
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
