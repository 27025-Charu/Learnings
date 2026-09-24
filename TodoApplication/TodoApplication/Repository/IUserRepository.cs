using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApplication.Model;

namespace TodoApplication.Repository
{
    internal interface IUserRepository
    {
        void Add(User user);
        bool Update(User user);
        bool Delete(string employeeId);
        User GetUserDetails(string employeeId);
        bool Check(string password, string email);
        User Exists(string employeeId);
    }
}
