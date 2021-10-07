using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopsRUs.Models
{
    public class Order
    {

        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string UserId { get; set; }
        public User User { get; set; }

        public ICollection<Item> Items { get; set; }

    }
}
