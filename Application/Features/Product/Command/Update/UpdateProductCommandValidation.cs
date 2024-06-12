using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Product.Command.Update
{
    public class UpdateProductCommandValidation : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidation()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id cannot be empty");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product Name cannot be empty");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Product Description cannot be empty");
            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Product Price cannot be empty");
            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Category Id cannot be empty");

        }
    }
}
