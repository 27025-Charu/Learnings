using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApplication.Services;
using TodoApplication.Model;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace TodoApplication.View
{
    internal class TaskView
    {
        private readonly TaskService _taskService;
        public TaskView(TaskService taskService)
        {
            _taskService = taskService;
        }
        public void ShowMenu()
        {
            if (UserView.CurrentUserId == null || UserView.CurrentUserId == string.Empty)
            {
                Console.WriteLine("You must be logged in to manage todos.");
                return;
            }
            while (true)
            {
                var listTask = _taskService.GetRecentTwoTasks();
                bool hasTasks = listTask.Count > 0;
                string listOfTodos = hasTasks ? string.Join("\n", listTask.Select(l => $"Task Id: {l.TaskId},Target date: {l.TargetDate},Task heading: {l.TaskHeading},Description: {l.Description},Recurrence: {l.TaskRecurrence}")) : "No Tasks(todos) added.";
                ConsoleHelper.PrintHeader("MY TODOS");
                Console.WriteLine($@"
{listOfTodos}
------------------------
1. Add Todo
2. Update Todo{(hasTasks ? string.Empty : " (disabled)")}
3. Delete Todo{(hasTasks ? string.Empty : " (disabled)")}
4. View All todos{(hasTasks ? string.Empty : " (disabled)")}
5. Sort todos based on Date{(hasTasks ? string.Empty : " (disabled)")}
6. Check for Completed Tasks{(hasTasks ? string.Empty : " (disabled)")}
7. Add todo to completed list{(hasTasks ? string.Empty : " (disabled)")}
8. Back
");
                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();
                if (!hasTasks && choice != "1" && choice != "8")
                {
                    Console.WriteLine("No tasks available. Please add a todo first.");
                    continue;
                }

                switch (choice)
                {
                    case "1":
                        AddTask();
                        break;
                    case "2":
                        UpdateTask();
                        break;
                    case "3":
                        DeleteTask();
                        break;
                    case "4":
                        ViewAllTask();
                        break;
                    case "5":
                        SortTaskByDate();
                        break;
                    case "6":
                        ShowCompletedTask();
                        break;
                    case "7":
                        AddToCompletedList();
                        break;
                    case "8":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        private void AddToCompletedList()
        {
            var list = _taskService.GetAllTasks();
            foreach (var task in list)
            {
                Console.WriteLine($@"{task.TaskId}: {task.TaskHeading}");
            }
            Console.WriteLine("Enter the task Id that needs to be added to completed List:");
            string taskId = (Console.ReadLine() ?? string.Empty).Trim();
            _taskService.AddToCompleted(taskId);
        }
        private void ShowCompletedTask()
        {
            var list = _taskService.ShowCompletedTask();
            if (list == null || list.Count == 0)
            {
                Console.WriteLine("Completed task list is empty.");
                return;
            }

            Console.WriteLine("Completed Tasks:");
            foreach (var task in list)
            {
                string taskId = task?.TaskId ?? string.Empty;
                string heading = task?.TaskHeading ?? string.Empty;
                string description = task?.Description ?? string.Empty;
                string recurrence = task?.TaskRecurrence.ToString() ?? string.Empty;
                string employeeId = task?.EmployeeId ?? string.Empty;
                string targetDateFormatted;

                try
                {
                    targetDateFormatted = task is null ? string.Empty : task.TargetDate.ToString("dd-MM-yyyy");
                }
                catch
                {
                    targetDateFormatted = task?.TargetDate.ToString() ?? string.Empty;
                }

                Console.WriteLine($@"Task Id: {taskId}
Task Heading: {heading}
Description: {description}
Recurrence: {recurrence}
Target date: {targetDateFormatted}
EmployeeId: {employeeId}
------------------------");
            }
        }

        private void SortTaskByDate()
        {
            _taskService.SortByDate();
        }

        private void ViewAllTask()
        {
            _taskService.GetAllTasks();
        }

        private void DeleteTask()
        {
            Console.WriteLine("Enter the task Id that needs to be deleted:");
            string taskId = (Console.ReadLine() ?? string.Empty).Trim();
            bool flag=_taskService.DeleteTask(taskId);
            if (flag)
            {
                Helper.DisplaySuccess("The Task is successfully deleted.");
            }
            else
            {
                Helper.DisplayError("The task is not deleted as there is no task with that Id.");
            }
        }

        private void UpdateTask()
        {
            Console.WriteLine("Enter the Task Id to be updated[Format: TASK001]:");
            string taskId = Console.ReadLine()?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(taskId))
            {
                Console.WriteLine("Task Id cannot be empty.");
                return;
            }

            if (!Regex.IsMatch(taskId, @"^TASK\d{3}$"))
            {
                Console.WriteLine("Task Id must be in the format TASK001 (exactly 'TASK' followed by 3 digits).");
                return;
            }

            if (!_taskService.Exists(taskId))
            {
                Console.WriteLine($"Task with ID {taskId} does not exist.");
                return;
            }

            var existingTask = _taskService.GetAllTasks().FirstOrDefault(t => t.TaskId == taskId);
            if (existingTask == null)
            {
                Console.WriteLine($"Unable to locate existing task with ID {taskId}.");
                return;
            }

            Console.WriteLine("Enter the new Task header (leave blank to keep existing):");
            string taskHeader = Console.ReadLine()?.Trim() ?? string.Empty;

            Console.WriteLine("Enter the new Description for the task (leave blank to keep existing): ");
            string description = Console.ReadLine()?.Trim() ?? string.Empty;

            Console.WriteLine($@"Enter the Recurrence mechanism (leave blank to keep existing)
[1] Daily
[2] Weekly
[3] Monthly
[4] Yearly
[5] None
");
            string recurrenceInput = Console.ReadLine()?.Trim() ?? string.Empty;
            Recurrence recurrence = existingTask.TaskRecurrence;
            if (!string.IsNullOrEmpty(recurrenceInput))
            {
                if (!int.TryParse(recurrenceInput, out int recurrenceValue) || !Enum.IsDefined(typeof(Recurrence), recurrenceValue))
                {
                    Console.WriteLine("Invalid recurrence selection.");
                    return;
                }
                recurrence = (Recurrence)recurrenceValue;
            }

            Console.WriteLine("Enter the Target date (dd-MM-yyyy) (leave blank to keep existing):");
            string targetDateInput = Console.ReadLine()?.Trim() ?? string.Empty;
            DateOnly targetDate = existingTask.TargetDate;
            if (!string.IsNullOrEmpty(targetDateInput))
            {
                if (!DateOnly.TryParseExact(targetDateInput, "dd-MM-yyyy", out DateOnly parsedDate))
                {
                    Console.WriteLine("Invalid date format. Please enter a valid date in dd-MM-yyyy format (e.g., 01-01-2002).");
                    return;
                }

                DateOnly today = DateOnly.FromDateTime(DateTime.Today);
                if (parsedDate <= today)
                {
                    Console.WriteLine("Target date must be a future date.");
                    return;
                }

                targetDate = parsedDate;
            }

            var updatedTask = new Todo
            {
                TaskId = taskId,
                EmployeeId = UserView.CurrentUserId,
                TaskHeading = string.IsNullOrEmpty(taskHeader) ? existingTask.TaskHeading : taskHeader,
                Description = string.IsNullOrEmpty(description) ? existingTask.Description : description,
                TaskRecurrence = recurrence,
                TargetDate = targetDate
            };

            try
            {
                _taskService.UpdateTask(updatedTask);
                Console.WriteLine($"Update request for Task ID {taskId} has been submitted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to update task: {ex.Message}");
            }
        }

        private void AddTask()
        {
            Console.WriteLine("Enter Task Id [Format: TASK001]:");
            string taskId = Console.ReadLine()?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(taskId))
            {
                Console.WriteLine("Task Id cannot be empty.");
                return;
            }

            if (!Regex.IsMatch(taskId, @"^TASK\d{3}$"))
            {
                Console.WriteLine("Task Id must be in the format TASK001 (exactly 'TASK' followed by 3 digits).");
                return;
            }

            if (_taskService.Exists(taskId))
            {
                Console.WriteLine($"A task with Id '{taskId}' already exists.");
                return;
            }

            string userid = UserView.CurrentUserId;
            Console.WriteLine("Enter the Task header:");
            string taskHeader = Console.ReadLine()?.Trim() ?? string.Empty;
            Console.WriteLine("Enter the Description for the task: ");
            string description = Console.ReadLine()?.Trim() ?? string.Empty;
            Console.WriteLine($@"Enter the Recurrence mechanism 
[1] Daily
[2] Weekly
[3] Monthly
[4] Yearly
[5] None
                ");
            string recurrenceInput = Console.ReadLine()?.Trim() ?? string.Empty;
            if (!int.TryParse(recurrenceInput, out int recurrenceValue) || !Enum.IsDefined(typeof(Recurrence), recurrenceValue))
            {
                Console.WriteLine("Invalid recurrence selection.");
                return;
            }
            Recurrence recurrence = (Recurrence)recurrenceValue;
            Console.WriteLine("The last target date for the recurrence (dd-MM-yyyy):");
            string targetDateInput = Console.ReadLine()?.Trim() ?? string.Empty;
            DateOnly targetDate;
            if (string.IsNullOrEmpty(targetDateInput) || !DateOnly.TryParseExact(targetDateInput, "dd-MM-yyyy", out targetDate))
            {
                Console.WriteLine("Invalid or missing date. Please enter a valid date in dd-MM-yyyy format (e.g., 01-01-2002).");
                return;
            }

            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            if (targetDate <= today)
            {
                Console.WriteLine("Target date must be a future date.");
                return;
            }

            var todo = new Todo
            {
                TaskId = taskId,
                EmployeeId = userid,
                TaskHeading = taskHeader,
                Description = description,
                TaskRecurrence = recurrence,
                TargetDate = targetDate
            };

            try
            {
                _taskService.AddTask(todo);
                Console.WriteLine($"Task '{taskId}' added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to add task: {ex.Message}");
            }
        }
    }
    }

