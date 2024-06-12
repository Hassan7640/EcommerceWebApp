using Domain.DTO;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Product.Command
{
    public class CreateProductCommand : IRequest<ProductDto>
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
    }
}
