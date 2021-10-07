using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopsRUs.Models
{
    public class Invoice
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string OrderId { get; set; }
        public Order Order { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal GrandTotal { get; set; }
    }
}
