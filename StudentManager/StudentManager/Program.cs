using System.Runtime.CompilerServices;
using StudentManager.View;

namespace StudentManager
{
    internal class Program
    {
        public static void Main()
        {
            Services.Studentmanager st = new Services.Studentmanager();

            View.ConsoleUI Ui = new(st);
            while (true)
            {
                Ui.DisplayMenu();
                var option = Ui.ReadOption();
                switch (option)
                {
                    case 1:
                        {
                            Console.WriteLine("Enter the Id, Name and Branch of the student");
                            int id = Convert.ToInt32(Console.ReadLine());
                            String name = Console.ReadLine();
                            String branch = Console.ReadLine();
                            st.Add(id, name, branch);
                            break;
                        }
                    case 2:
                        Ui.DisplayAll(st);
                        break;
                    case 3:
                        Ui.DisplayById();
                        break;
                    case 4:
                        Ui.RemoveById();
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }

            }
        }
    }
}
