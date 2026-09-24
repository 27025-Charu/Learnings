using System;
using System.Collections.Generic;
using System.Text;
using VehicleWash.Models;

namespace VehicleWash.Repository
{
    internal interface IUser
    {
        void Add(User user);
        bool Update(User user);
        bool Delete(Guid guId);
        User GetUserDetails(Guid guId);
    }
}
