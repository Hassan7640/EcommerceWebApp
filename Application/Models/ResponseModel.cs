using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
   public class ResponseModel
    {
        public string ResponseCode { get; set; }
        public bool Status { get; set; }

        public object Data { get; set; }

        public string Message { get; set; }
    }
}
