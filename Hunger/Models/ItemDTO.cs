using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hunger.Models
{
    public class ItemDTO
    {
        public int id { get; set; }

        [Required]
        public string name { get; set; }
        [Required]
        public Nullable<int> quantity { get; set; }

        [ExpireDateValidation(ErrorMessage = "Expire Date Must Be Atleast One Day Higher Then Request Date")]
        public DateTime ExpireDate { get; set; }
        public Nullable<int> RequestID { get; set; }
    }
}