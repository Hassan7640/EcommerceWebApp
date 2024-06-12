using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Product.Command.Create
{
    public class CreateProductCommandValidation : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().NotNull().WithMessage("Product Name cannot be empty");
            RuleFor(x => x.Description)
                .NotEmpty().NotNull().WithMessage("Product Description cannot be empty");
            RuleFor(x => x.Price)
                .NotEmpty().NotNull().WithMessage("Product Price cannot be empty");
            RuleFor(x => x.CategoryId)
                .NotEmpty().NotNull().WithMessage("Category Id cannot be empty");

        }

    }
}
