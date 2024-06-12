﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
   public class ExceptionResponse
    {
        public string Title { get; set; }

        public object Data { get; set; }

        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
