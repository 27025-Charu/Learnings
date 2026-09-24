using MultiUserNotification.Model;

namespace MultiUserNotification.Repository
{
    internal class InMemoryRepository
    {
        List<User> users;
        public InMemoryRepository()
        {
            users = new List<User>();
        }
        internal void AddUser(User user)
        {
            users.Add(user);
        }
        internal string? GetPassById(int id)
        {
            var user = users.FirstOrDefault(u => u.UserId == id);
            return user?.Password;
        }
        internal bool CheckUserExists(int id)
        {
            return users.Any(u => u.UserId == id);
        }
    }
}