using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApplication.Model;
using TodoApplication.Repository;

namespace TodoApplication.Services
{
    internal class UserAuthService
    {
        private readonly IUserRepository _userRepository;
        private UserRepository userRepository;

        public UserAuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public bool Login(string password, string employeeId)
        {
            return _userRepository.Check(password, employeeId);
        }

        public bool Register(User user)
        {
            var existingId = _userRepository.Exists(user.EmployeeId);
            if (existingId != null)
            {
                return false;
            }
            _userRepository.Add(user);
            return true;
        }

        public User GetUser(string employeeId)
        {
            return _userRepository.GetUserDetails(employeeId);
        }

        public bool UpdateUser(User user)
        {
            return _userRepository.Update(user);
        }

        public bool DeleteUser(string employeeId)
        {
            return _userRepository.Delete(employeeId);
        }

        internal bool Exists(string id)
        {
            var user=_userRepository.Exists(id);
            if (user != null) 
            {
                return true;
            }
            return false;
        }
    }
}
