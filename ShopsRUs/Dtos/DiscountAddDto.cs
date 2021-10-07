using ShopsRUs.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopsRUs.Dtos
{
    public class DiscountAddDto
    {
        [Required(ErrorMessage = "Discount rate is required")]
        [Range(0.1, 100, ErrorMessage = "Dsicount rate should be between 0.1 and 100")]
        public decimal DiscountRate { get; set; }

        [Required(ErrorMessage = "User type is required")]
        [DataType(DataType.Text)]
        public string UserType { get; set; }
    }
}
