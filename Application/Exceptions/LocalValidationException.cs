using Application.Contracts.Infrastructure;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
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

        public LocalValidationException(IEnumerable<IdentityError> failures)
          : this()
        {
            Errors = failures
                .GroupBy(e => e.Code, e => e.Description)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }

    }
}
