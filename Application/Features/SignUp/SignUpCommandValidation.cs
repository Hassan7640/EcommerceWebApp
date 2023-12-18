using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SignUp
{
    public class SignUpCommandValidation : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidation() {
            RuleFor(u => u.UserName)
                .NotEmpty().WithMessage("{UserName} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{UserName} must not exceed 50 characters.");

            RuleFor(f => f.FirstName)
                .NotEmpty().WithMessage("FirstName is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("FirstName must not exceed 50 characters.");

            RuleFor(l => l.LastName)
                .NotEmpty().WithMessage("LastName is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("LastName must not exceed 50 characters.");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("Password is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("Password must not exceed 50 characters.");

            RuleFor(c => c.ConfirmPassword)
               .NotEmpty().WithMessage("ConfirmPassword is required.")
               .NotNull()
               .MaximumLength(50).WithMessage("ConfirmPassword must not exceed 50 characters.");


        }
    }
    
}
