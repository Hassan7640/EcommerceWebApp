using Domain.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Category.Query
{
    public class FindCategoryByIdQuery : IRequest<CategoryDto>
    {
        public int Id { get; set; }
    }
}
