using MultiUserNotification.Model;
using MultiUserNotification.Repository;

namespace MultiUserNotification.Services
{
    internal class Service
    {
        private InMemoryRepository _repository;
        public Service(InMemoryRepository repository)
        {
            _repository = repository;
        }

        internal void AddUser(User user)
        {
            user.Password = PasswordHashing.HashPassword(user.Password);
            _repository.AddUser(user);
        }

        internal bool CheckLoginUser(int id, string? password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }
            string? storedHash = _repository.GetPassById(id);
            if (storedHash == null)
            {
                return false;
            }
            if (VerifyPass(storedHash, password))
            {
                return true;
            }
            return false;
        }

        private bool VerifyPass(string storedHash, string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                return PasswordHashing.PassVerification(storedHash, password);
            }
            return false;
        }
    }
}
