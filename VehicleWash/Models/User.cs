using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleWash.Models
{
    public class User
    {
        public Guid Id;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string password { get; set; } 
        public string phoneNumber { get; set; }
    }
}
