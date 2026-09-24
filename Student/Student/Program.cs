namespace Student
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Student student = new Student();
            //student.Name = "A";
            //student.Id = 123;
            //student.Description = "10th";
            //Student student1 = new Student(123, "A", "10th");
            //IBird crow = new Crow();
            //crow.Fly();
            //IBird dove = new Dove();
            //dove.Fly();
            //Console.ReadLine();
            Print print = new PrintChild();
            print.PrintMessage();
            Console.ReadLine();
        }
    }
    //}
    //public class Student
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public string Description { get; set; }

    //    public Student() : this(0, string.Empty, string.Empty)
    //    {
    //    }

    //    public Student(int id, string name, string description)
    //    {
    //        this.Id = id;
    //        this.Name = name;
    //        this.Description = description;
    //    }
    //}
}

