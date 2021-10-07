using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopsRUs.Models
{
    public class Discount
    {

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public decimal DiscountRate { get; set; }

    }
}
