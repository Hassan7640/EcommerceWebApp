using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApp.Filters
{
    public class Errors
    {
        public Errors()
        {
            Message = new List<string>();
        }

        public List<string> Message { get; set; }
    }
}
