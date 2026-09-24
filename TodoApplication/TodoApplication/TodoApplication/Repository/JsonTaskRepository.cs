using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TodoApplication.Model;

namespace TodoApplication.Repository
{
    internal class JsonTaskRepository : ITaskRepository
    {
        private readonly string _liveFilePath = "todos.json";
        private readonly string _completedFilePath = "completed_todos.json";
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        private List<Todo> LoadActiveTasks()
        {
            if (!File.Exists(_liveFilePath)) return new List<Todo>();
            string json = File.ReadAllText(_liveFilePath);
            return JsonSerializer.Deserialize<List<Todo>>(json, _jsonOptions) ?? new List<Todo>();
        }

        private void SaveActiveTasks(List<Todo> todos)
        {
            string json = JsonSerializer.Serialize(todos, _jsonOptions);
            File.WriteAllText(_liveFilePath, json);
        }

        public List<Todo> GetCompletedTasks()
        {
            if (!File.Exists(_completedFilePath)) return new List<Todo>();
            string json = File.ReadAllText(_completedFilePath);
            return JsonSerializer.Deserialize<List<Todo>>(json, _jsonOptions) ?? new List<Todo>();
        }

        private void SaveCompletedTasks(List<Todo> completed)
        {
            string json = JsonSerializer.Serialize(completed, _jsonOptions);
            File.WriteAllText(_completedFilePath, json);
        }

        public void AddTask(Todo todo)
        {
            var todos = LoadActiveTasks();
            todos.Add(todo);
            SaveActiveTasks(todos);
        }

        public List<Todo> GetAllTasks()
        {
            return LoadActiveTasks();
        }

        public Todo GetTaskById(string id)
        {
            return LoadActiveTasks().FirstOrDefault(t => t.TaskId == id);
        }

        public bool Exists(string id)
        {
            return LoadActiveTasks().Any(t => t.TaskId == id);
        }

        public List<Todo> LastTwoTasks()
        {
            var todos = LoadActiveTasks();
            return todos.Skip(Math.Max(0, todos.Count - 2)).ToList();
        }

        public bool UpdateTask(Todo todo)
        {
            var todos = LoadActiveTasks();
            var index = todos.FindIndex(t => t.TaskId == todo.TaskId);
            if (index >= 0)
            {
                todos[index] = todo;
                SaveActiveTasks(todos);
                return true;
            }
            return false;
        }

        public bool DeleteTask(string id)
        {
            var todos = LoadActiveTasks();
            var todo = todos.FirstOrDefault(t => t.TaskId == id);
            if (todo != null)
            {
                todos.Remove(todo);
                SaveActiveTasks(todos);
                return true;
            }
            return false;
        }

        public void AddToCompleted(Todo todo)
        {
            var completedList = GetCompletedTasks();
            completedList.Add(todo);
            SaveCompletedTasks(completedList);
        }
        public void ProcessRecurringTasks()
        {
            var todos = LoadActiveTasks();
            var today = DateOnly.FromDateTime(DateTime.Now);
            var dueTasks = todos.Where(t => t.RecurrenceDate != default && t.RecurrenceDate <= today).ToList();

            if (!dueTasks.Any()) return;

            var completedList = GetCompletedTasks();

            foreach (var task in dueTasks)
            {
                var completionRecord = new Todo
                {
                    TaskId = task.TaskId,
                    TaskHeading = task.TaskHeading,
                    RecurrenceDate = task.RecurrenceDate,
                };
                completedList.Add(completionRecord);
                task.RecurrenceDate = task.RecurrenceDate.AddDays(7);
            }

            SaveCompletedTasks(completedList);
            SaveActiveTasks(todos);
        }
    }
}
