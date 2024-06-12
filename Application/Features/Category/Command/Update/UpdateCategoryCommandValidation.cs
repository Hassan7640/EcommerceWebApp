using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Category.Command.Update
{
    public class UpdateCategoryCommandValidation : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidation()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id cannot be empty");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category Name cannot be empty");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Category Description cannot be empty");

        }

    }
}
