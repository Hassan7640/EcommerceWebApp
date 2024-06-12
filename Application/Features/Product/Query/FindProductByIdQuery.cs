using Domain.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Product.Query
{
    public class FindProductByIdQuery : IRequest<ProductDto>
    {
        public int Id { get; set; }
    }
}
