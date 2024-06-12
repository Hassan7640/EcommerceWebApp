using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence
{
    public interface ICategoryRepository
    {
        Task<CategoryDto> CreateCategory(CategoryDto categoryDto);
        Task<CategoryDto> FindCategoryById(int id);
        Task<CategoryDto> UpdateCategory(int id, CategoryDto categoryDto);
        Task<CategoryDto> DeleteCategoryById(int id);

    }
}
