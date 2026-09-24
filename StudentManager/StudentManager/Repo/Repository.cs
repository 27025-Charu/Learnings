using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using StudentManager.Model;


namespace StudentManager.Repo
{
    internal class Repository
    {
        List<Model.Student> students = new ();
        public Repository()
        {
          
        }

        public void AddStudent(Student s)
        {
            students.Add(s);
        }
        public Model.Student GetById(int id)
        {
            return students.FirstOrDefault(s => s.Id == id);    
        }

        public List<Student> GetAll()
        {
            return students;

        }
        public void Delete(int id)
        {
            var st = GetById(id);
            if (st != null)
            {
                students.Remove(st);
            }

        }


    }
}
