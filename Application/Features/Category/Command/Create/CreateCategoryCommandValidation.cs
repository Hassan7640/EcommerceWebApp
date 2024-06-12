using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Category.Command.Create
{
    public class CreateCategoryCommandValidation : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidation()
        {
            RuleFor(x => x.Name)
                .NotNull().NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(x => x.Description)
                .NotNull().NotEmpty().WithMessage("Description cannot be empty");


        }
    }
}
