using Domain.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Category.Command.Delete
{
    public class DeleteCategoryByIdCommand : IRequest<CategoryDto>
    {
        public int Id { get; set; }
    }
}
