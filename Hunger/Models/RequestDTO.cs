using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hunger.Models
{
    public class RequestDTO
    {
        public int id { get; set; }
        public string req_time { get; set; }
        public string col_time { get; set; }
        public string status { get; set; }
        public Nullable<int> restaurent_id { get; set; }
        public Nullable<int> employee_id { get; set; }
    }
}