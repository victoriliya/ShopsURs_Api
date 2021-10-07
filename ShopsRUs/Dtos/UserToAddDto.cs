using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopsRUs.Dtos
{
    public class UserToAddDto
    {

        [Required(ErrorMessage = "Firstname is required")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lastname is required")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
    }
}
