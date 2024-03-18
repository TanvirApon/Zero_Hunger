using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hunger.Models
{
    public class ExpireDateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime expDate = (DateTime)value;

            if (expDate >= DateTime.Now.AddDays(1))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("The expiration date must be 1 day higher than the current date and time."); ;
        }
    }
}