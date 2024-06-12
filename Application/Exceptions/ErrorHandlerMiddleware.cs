using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Exceptions
{
   public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke (HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception error)
            {
                var response = context.Response;
                IDictionary<string, string[]> validationErrors = new Dictionary<string, string[]>();
                response.ContentType = "application/json";

                switch (error)
                {
                    case CustomException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case LocalValidationException e:
                        validationErrors = e.Errors;
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                var result = JsonSerializer.Serialize(new { message = error?.Message, Errors = validationErrors });
                 await response.WriteAsync(result);
            }
        }

    }
}
