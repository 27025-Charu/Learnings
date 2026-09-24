using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManager.Model
{
    internal class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Branch { get; set; } = string.Empty;
        
        public Student(int id, string name, string branch)
        {
            Id = id;
            Name = name;
            Branch = branch;
        }
    }
}
