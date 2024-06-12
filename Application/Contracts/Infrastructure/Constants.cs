using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Infrastructure
{
    public static class Constants
    {
        public static class ResponseCode
        {
            public const string Succesfull = "00";
            public const string Failed = "01";
            public const string ValidationError = "02";
            public const string NotFound = "03";
            public const string WrongCredentials = "04";

        }

        public const string ValidationMessage = "One or more validation failures occured";

        public static ResponseModel BadResponse(string message = "failed")
        {
            ResponseModel badResponse = new ResponseModel
            {
                Message = message,
                Data = null,
                ResponseCode = ResponseCode.Failed,
                Status = false
            };
            return badResponse;
        }
    }
}
