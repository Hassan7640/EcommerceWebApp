using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ResponseBaseModel
    {
        public string Message { get; set; }
        public string ResponseCode { get; set; }
        public bool RequestStatus { get; set; }
    }
}
