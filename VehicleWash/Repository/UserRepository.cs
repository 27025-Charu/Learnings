using System;
using System.Collections.Generic;
using System.Text;
using VehicleWash.Models;

namespace VehicleWash.Repository
{
    public class UserRepository : IUser
    {
        Dictionary<Guid, User> users = new();
        public void Add(User user)
        {
            users.Add(user.Id, user);
        }

        public bool Delete(Guid userId)
        {
            if (users.Remove(userId))
            {
                return true;
            }

            return false;
        }

        public User GetUserDetails(Guid userId)
        {
            if (users.TryGetValue(userId, out User user))
            {
                return user;
            }

            return null;
        }

        public bool Update(User user)
        {
            if (!users.ContainsKey(user.Id))
            {
                return false;
            }

            users[user.Id] = user;
            return true;
        }

        public Guid getIdByEmail(string email)
        {
            foreach (var user in users.Values)
            {
                if (user.EmailId == email)
                {
                    return user.Id;
                }
            }
            return Guid.Empty;
        }
        public bool Check(string pass, string email)
        {
            foreach (var user in users.Values)
            {
                if (user.EmailId == email)
                {
                    return user.password.Equals(pass);
                }
            }
            return false;
        }
    }
}
