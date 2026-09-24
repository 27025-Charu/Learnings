using System.Security.Cryptography.X509Certificates;

namespace Linqq
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            //List<Integers> StudentId = new();
            //StudentId.Add({ 2,4,5,6,7,8,1234,213,24,45});
            //SampleClass sample = new SampleClass
            //{
            //    sample = 10,
            //    name = "xxx",
            //}
            Student student = new Student
            {
                FirstName = "Name",
                LastName = "lastName",
                Age = 20,
                College = "Easwari",
            };
            List<Student> students = new List<Student>();
            students.Add(student);
            foreach (var l in students)
            {
                Console.WriteLine(l.FirstName);
                Console.WriteLine(l.LastName);
                Console.WriteLine(l.Age);
                Console.WriteLine(l.College);
            }
            students.Add(new Student
            {
                FirstName = "Name",
                LastName = "Last",
                Age = -19,
                College = "Easwari"
            });
            students.Add(new Student
            {
                FirstName = "Name",
                LastName = "Last",
                Age = 40,
                College = "Easwari"
            });
            students.Add(new Student
            {
                FirstName = "Name",
                LastName = "Last",
                Age = 200,
                College = "Easwari"
            });
            students.Add(new Student
            {
                FirstName = "Name",
                LastName = "Last",
                Age = 21,
                College = "Easwari"
            });
            foreach (var l in students)
            {
                Console.WriteLine(l.FirstName);
                Console.WriteLine(l.LastName);
                Console.WriteLine(l.Age);
                Console.WriteLine(l.College);
            }
            IEnumerable<Student> SortByAge(List<Student> l)=> students.Where(student => student.Age >18).OrderBy(student=>student.Age);
            Console.Write("-----------------------------------");
            foreach(var l in SortByAge(students))
            {
                Console.WriteLine(l.FirstName);
                Console.WriteLine(l.LastName);
                Console.WriteLine(l.Age);
                Console.WriteLine(l.College);
            }
        }
    }
    public class Student
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        private int age;
        public int Age
        {
            get
            {
                if (age < 18)
                {
                    Console.WriteLine("The age is not valid. ");
                    return 0;
                }
                return age;
            }
            set
            {
                if (value < 0)
                {
                    Console.WriteLine("The age is not valid. ");
                    age = 1;
                }
                else
                {
                    age = value;
                }
            }
        }
        public string? College { get; set; }
    }
}
//}
//public class SampleClass
//{
//    public required int Sample { get; set; }
//    public required string Name { get; set; }
//}
