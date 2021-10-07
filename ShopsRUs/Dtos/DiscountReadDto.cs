using ShopsRUs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopsRUs.Dtos
{
    public class DiscountReadDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public decimal DiscountRate { get; set; }
        public string UserTypeName {  get; set; }
    }
}
