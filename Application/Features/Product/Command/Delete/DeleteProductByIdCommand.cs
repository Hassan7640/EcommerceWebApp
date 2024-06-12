using Domain.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Product.Command.Delete
{
    public class DeleteProductByIdCommand : IRequest<ProductDto>
    {
        public int Id { get; set; }
    }
}
