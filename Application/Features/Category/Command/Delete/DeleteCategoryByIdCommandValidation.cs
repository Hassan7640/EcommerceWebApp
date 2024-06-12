using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Category.Command.Delete
{
    public class DeleteCategoryByIdCommandValidation : AbstractValidator<DeleteCategoryByIdCommand>
    {
        public DeleteCategoryByIdCommandValidation()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id cannot be empty");
        }
    }
}
