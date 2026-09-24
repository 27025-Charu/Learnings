using VehicleWash.Repository;
using VehicleWash.Models;
using System;

namespace VehicleWash.Services
{
    internal class UserAuthService
    {
        private readonly UserRepository _userRepository;

        public UserAuthService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool Login(string password, string email)
        {
            return _userRepository.Check(password, email);
        }

        public bool Register(User user)
        {
            var existingId = _userRepository.getIdByEmail(user.EmailId);
            if (existingId != Guid.Empty)
            {
                return false;
            }
            _userRepository.Add(user);
            return true;
        }

        public User GetUser(Guid userId)
        {
            return _userRepository.GetUserDetails(userId);
        }

        public bool UpdateUser(User user)
        {
            return _userRepository.Update(user);
        }

        public bool DeleteUser(Guid userId)
        {
            return _userRepository.Delete(userId);
        }

        public Guid GetId(string email)
        {
            return _userRepository.getIdByEmail(email);
        }
    }
}