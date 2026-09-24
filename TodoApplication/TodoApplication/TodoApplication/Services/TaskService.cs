using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApplication.Model;
using TodoApplication.Repository;

namespace TodoApplication.Services
{
    internal class TaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public List<Todo> GetRecentTwoTasks()
        {
            return _taskRepository.LastTwoTasks() ?? new List<Todo>();
        }

        public List<Todo> GetTasksByUserId(string currentUserId)
        {
            var todo = _taskRepository.GetTaskById(currentUserId);
            return todo != null ? new List<Todo> { todo } : new List<Todo>();
        }

        public bool AddTask(Todo todo)
        {
            if (todo == null) return false;
            if (_taskRepository.Exists(todo.TaskId))
            {
                return false;
            }

            _taskRepository.AddTask(todo);
            return true;
        }

        public bool UpdateTask(Todo todo)
        {
            if (todo == null) return false;

            if (_taskRepository.Exists(todo.TaskId))
            {
                return _taskRepository.UpdateTask(todo);
            }
            return false;
        }

        public List<Todo> GetAllTasks()
        {
            return _taskRepository.GetAllTasks() ?? new List<Todo>();
        }

        public Todo GetTodo(string id)
        {
            return _taskRepository.GetTaskById(id);
        }

        public bool DeleteTask(string id)
        {
            return _taskRepository.DeleteTask(id);
        }

        public bool Exists(string taskId)
        {
            return _taskRepository.Exists(taskId);
        }

        public List<Todo> SortByDate()
        {
            var list = _taskRepository.GetAllTasks();
            return list != null ? list.OrderBy(l => l.TargetDate).ToList() : new List<Todo>();
        }

        public void AddToCompleted(string taskId)
        {
            var todo = _taskRepository.GetTaskById(taskId);
            if (todo == null) return;

            _taskRepository.AddToCompleted(todo);
            _taskRepository.DeleteTask(taskId);
        }

        public List<Todo> ShowCompletedTask()
        {
            return _taskRepository.GetCompletedTasks() ?? new List<Todo>();
        }
    }
}
