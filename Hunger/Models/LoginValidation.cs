using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hunger.Models
{
    public class LoginValidation
    {
        [Required]
        public string email { get; set; }

        [Required]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email format")]
        public string password { get; set; }
        [Required]
        public string UserType { get; set; }
    }
}