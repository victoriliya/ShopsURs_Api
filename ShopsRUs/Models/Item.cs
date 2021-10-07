using System;

namespace ShopsRUs.Models
{
    public class Item
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string OrderId { get; set; }
        public Order Order { get; set; }

        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public ItemType ItemType { get; set; }

    }
}