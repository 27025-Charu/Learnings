using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TodoApplication.Model;

namespace TodoApplication.Repository
{
    internal class TaskRepository : ITaskRepository
    {
        private static readonly List<Todo> _todos = new List<Todo>();
        private static readonly List<Todo> _completed=new List<Todo>();
        public void AddTask(Todo todo)
        {
            _todos.Add(todo);
        }

        public bool DeleteTask(string id)
        {
            var todo = _todos.FirstOrDefault(t => t.TaskId == id);
            if (todo != null)
            {
                _todos.Remove(todo);
                return true;
            }
            return false;
        }

        public bool Exists(string id)
        {
            if (_todos.Any(t => t.TaskId == id))
            {
                return true;
            }
            return false;
        }

        public List<Todo> GetAllTasks()
        {
            ProcessCompletedTasks();
            return new List<Todo>(_todos);
        }

        public Todo GetTaskById(string id)
        {
            var index = _todos.FindIndex(t => t.TaskId == id);
            if (index >= 0)
            {
                return _todos[index];
            }
            return null;
        }

        public List<Todo> LastTwoTasks()
        {
            return _todos.Skip(Math.Max(0, _todos.Count - 2)).ToList();
        }

        public bool UpdateTask(Todo todo)
        {
            var index = _todos.FindIndex(t => t.TaskId == todo.TaskId);
            if (index >= 0)
            {
                _todos[index] = todo;
                return true;
            }
            return false;
        }
        public void AddToCompleted(Todo todo)
        {
            _completed.Add(todo);
        }

        public List<Todo> GetCompletedTasks()
        {
            ProcessCompletedTasks();
            return new List<Todo>(_completed);
        }
        private void ProcessCompletedTasks()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var dueTasks = _todos.Where(t => t.RecurrenceDate != default && t.RecurrenceDate <= today).ToList();
            if (!dueTasks.Any())
            {
                return;
            }

            foreach (var task in dueTasks)
            {
                var completionRecord = new Todo
                {
                    TaskId = task.TaskId,
                    TaskHeading = task.TaskHeading,
                    RecurrenceDate = task.RecurrenceDate,
                };
                _completed.Add(completionRecord);
                task.RecurrenceDate = task.RecurrenceDate.AddDays(7);
            }
        }

    }
}
