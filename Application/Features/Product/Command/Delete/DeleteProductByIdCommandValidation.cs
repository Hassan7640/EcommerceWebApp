using Domain.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Product.Command.Delete
{
    public class DeleteProductByIdCommandValidation : AbstractValidator<ProductDto>
    {
        public DeleteProductByIdCommandValidation()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id cannot be empty");
        }
    }
}
