using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace StudentManager.Services
{
    internal class Studentmanager
    {
        private readonly Repo.Repository repo = new();

        public void Add(int id, string name, string branch)
        {
            var student = new Model.Student(id, name, branch);
            repo.AddStudent(student);
        }

        public Model.Student GetById(int id)
        {
            return repo.GetById(id);
        }

        public bool DeleteStudent(int id)
        {
            repo.Delete(id);
            // Assuming Delete should return a bool, but repo.Delete returns void.
            // You may want to update repo.Delete to return a bool, or handle accordingly.
            return true;
        }

        public List<Model.Student> GetAll()
        {
            return repo.GetAll();
        }
    }
}
