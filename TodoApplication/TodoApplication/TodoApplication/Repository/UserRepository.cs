using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApplication.Model;

namespace TodoApplication.Repository
{
    internal class UserRepository : IUserRepository
    {
        private static readonly List<User> _users = new List<User>();

        public User Exists(string employeeId)
        {
            return _users.FirstOrDefault(u => u.EmployeeId == employeeId);
        }

        public void Add(User user)
        {
            _users.Add(user);
        }

        public bool Check(string password, string employeeId)
        {
            return _users.Any(u => u.EmployeeId == employeeId && u.Password == password);
        }

        public User GetUserDetails(string employeeId)
        {
            return Exists(employeeId);
        }

        public bool Update(User user)
        {
            var existing = Exists(user.EmployeeId);
            if (existing == null)
            {
                return false;
            }

            existing.Name = user.Name;
            existing.Password = user.Password;
            return true;
        }

        public bool Delete(string employeeId)
        {
            var user = Exists(employeeId);
            if (user == null)
            {
                return false;
            }
            return _users.Remove(user);
        }
    }
}