using Domain.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Category.Command.Create
{
    public class CreateCategoryCommand : IRequest<CategoryDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
