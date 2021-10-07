using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopsRUs.Models
{
    public class UserType
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string DiscountId { get; set; }
        public Discount Discount { get; set; }

        public string UserTypeName { get; set; }

    }
}
