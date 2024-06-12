using Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApp.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandler;

        public ApiExceptionFilter()
        {
            _exceptionHandler = new Dictionary<Type, Action<ExceptionContext>>
            {
                  {typeof(LocalValidationException), HandleValidationException},
                  {typeof(CustomException), HandleNotFoundException},
                  {typeof(AlreadyExistException),HandleAlreadyExistException},
                  {typeof(NotFoundException), HandleGenericInputException}
            };
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);
            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (_exceptionHandler.ContainsKey(type))
            {
                _exceptionHandler[type].Invoke(context);
                return;
            }
            HandleUnknownException(context);
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            ExceptionResponse exceptionResponse = new ExceptionResponse
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Message = "An error occured while processing your request.",
                Title = "Server Error"
            };
            context.Result = new ObjectResult(exceptionResponse)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
            context.ExceptionHandled = true;
        }

        private void HandleValidationException(ExceptionContext context)
        {
            var exception = context.Exception as LocalValidationException;

            ExceptionResponse exceptionResponse = new ExceptionResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "An error occured while processing your request.",
                Title = "Input error",
                Data = GetLocalValidationExceptionErrors(exception)
            };
            context.Result = new BadRequestObjectResult(exceptionResponse);
            context.ExceptionHandled = true;
        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            var exception = context.Exception as CustomException;

            ExceptionResponse exceptionResponse = new ExceptionResponse
            {
                Title = "Not Found",
                StatusCode = StatusCodes.Status404NotFound,
                Message = "The specified resource was not found.",
                Data = exception.Message
            };
            context.Result = new NotFoundObjectResult(exceptionResponse);
            context.ExceptionHandled = true;
        }

        private void HandleGenericInputException(ExceptionContext context)
        {
            var exception = context.Exception as CustomException;
            ExceptionResponse exceptionResponse = new ExceptionResponse
            {
                Title = "Input error",
                StatusCode = StatusCodes.Status400BadRequest,
                Message = exception.Message,
                Data = exception.Data
            };
            context.Result = new BadRequestObjectResult(exceptionResponse);
            context.ExceptionHandled = true;
        }

        private Errors GetLocalValidationExceptionErrors(LocalValidationException localValidationException)
        {
            Errors errors = new Errors();
            if (localValidationException.Errors.Count > 0)
            {
                foreach(var error in localValidationException.Errors)
                {
                    errors.Message.AddRange(error.Value);
                }
            }
            return errors;
        }

        private void HandleAlreadyExistException(ExceptionContext context)
        {
            var exception = context.Exception as AlreadyExistException;
            ExceptionResponse exceptionResponse = new ExceptionResponse
            {
                Title = "Already Exists",
                StatusCode = StatusCodes.Status400BadRequest,
                Message = exception.Message,
                Data = exception.Data
            };
            context.Result = new BadRequestObjectResult(exceptionResponse);
            context.ExceptionHandled = true;
        }
        }
    
}
