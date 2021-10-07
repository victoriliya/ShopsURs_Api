using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopsRUs.Models
{
    public class User
    {

        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Invoice> Invoice {  get; set; }
        public ICollection<Order> Orders { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public string UserTypeId { get; set; }

        public UserType UserType { get; set; }

    }
}
