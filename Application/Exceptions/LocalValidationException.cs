using Application.Contracts.Infrastructure;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
   public class LocalValidationException : Exception
    {
        public LocalValidationException() : base (Constants.ValidationMessage)
        {
            Errors = new Dictionary<string,string[]>();
        }

        public LocalValidationException(IDictionary<string, string[]> errors) : this()
        {
            Errors = errors;
        } 

        public IDictionary<string, string[]> Errors { get; }

    }
}
