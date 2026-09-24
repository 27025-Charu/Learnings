using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApplication.Model;

namespace TodoApplication.Repository
{
    internal interface ITaskRepository
    {
        public List<Todo> GetAllTasks();
        public Todo GetTaskById(string id);
        public void AddTask(Todo todo);
        public bool DeleteTask(string id);
        public bool UpdateTask(Todo todo);
        public List<Todo> GetCompletedTasks();
        public List<Todo> LastTwoTasks();
        bool Exists(String id);
        void AddToCompleted(Todo todo);
    }
}
