using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace StudentManager.View
{
    internal class ConsoleUI
    {
        private Services.Studentmanager studentmanager = new();
        public ConsoleUI(Services.Studentmanager studentmanager1)
        {
            studentmanager = studentmanager1;
        }
        public void DisplayMenu()
        {
            Console.WriteLine("1. Add 2. GetAll 3.Get By ID 4. Remove by Id 5. Exit \n Enter option");
        }
        public int ReadOption()
        {
            return Convert.ToInt32(Console.ReadLine());
        }
        public void DisplayAll()
        {
            var lists = studentmanager.GetAll();
            foreach (var l in lists)
            {
                DisplayStudent(l);
            }
        }
        public void DisplayById()
        {
            Console.WriteLine("Enter id:");
            int id=Convert.ToInt32(Console.ReadLine());
            var Student = studentmanager.GetById(id);
            DisplayStudent(Student);
        }

        public void RemoveById()
        {
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(studentmanager.DeleteStudent(id));
        }

        public void DisplayStudent(Model.Student i)
        {
            if (i != null)
            {
                Console.WriteLine($"ID:{i.Id}, Name:{i.Name}, Branch:{i.Branch}");
            }
        }

    }
}
