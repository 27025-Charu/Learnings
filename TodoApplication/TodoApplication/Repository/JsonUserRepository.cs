using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TodoApplication.Model;

namespace TodoApplication.Repository
{
    internal class JsonUserRepository : IUserRepository
    {
        private readonly string _filePath = "users.json";

        private List<User> LoadAllUsers()
        {
            if (!File.Exists(_filePath)) return new List<User>();
            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }

        private void SaveAllUsers(List<User> users)
        {
            string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public User Exists(string employeeId) => LoadAllUsers().FirstOrDefault(u => u.EmployeeId == employeeId);

        public void Add(User user)
        {
            var users = LoadAllUsers();
            users.Add(user);
            SaveAllUsers(users);
        }

        public bool Check(string password, string employeeId) =>  LoadAllUsers().Any(u => u.EmployeeId == employeeId && u.Password == password);

        public User GetUserDetails(string employeeId) => Exists(employeeId);

        public bool Update(User user)
        {
            var users = LoadAllUsers();
            var existing = users.FirstOrDefault(u => u.EmployeeId == user.EmployeeId);
            if (existing == null) return false;

            existing.Name = user.Name;
            existing.Password = user.Password;
            SaveAllUsers(users);
            return true;
        }

        public bool Delete(string employeeId)
        {
            var users = LoadAllUsers();
            var user = users.FirstOrDefault(u => u.EmployeeId == employeeId);
            if (user == null) return false;

            users.Remove(user);
            SaveAllUsers(users);
            return true;
        }
    }
}