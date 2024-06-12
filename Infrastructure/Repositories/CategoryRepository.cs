using Application.Contracts.Persistence;
using Application.Exceptions;
using AutoMapper;
using Domain.DTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryRepository> _logger;

        public CategoryRepository(EcomDbContext ecomDbContext, IMapper mapper, ILogger<CategoryRepository> logger) : base(ecomDbContext)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CategoryDto> CreateCategory(CategoryDto categoryDto)
        {
            try
            {
                var category = await _ecomDbContext.Categories.FirstOrDefaultAsync(c => c.Name.ToLower().Trim() == categoryDto.Name.ToLower().Trim());
                if (category == null)
                {
                    var categoryMapper = _mapper.Map<Category>(categoryDto);
                    _ecomDbContext.Categories.Add(categoryMapper);
                    int result = await _ecomDbContext.SaveChangesAsync();
                    if (result > 0)
                    {
                        var createdCategory = await _ecomDbContext.Categories.FirstOrDefaultAsync(c => c.Name.ToLower().Trim() == categoryDto.Name.ToLower().Trim());
                        var categoryResponse = _mapper.Map<CategoryDto>(createdCategory);
                        return categoryResponse;
                    }
                    else
                    {
                        throw new CustomException("Category could not be saved. Please try again.");

                    }
                }
                else
                {
                    throw new AlreadyExistException("Product already exists.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Category Repository failed {JsonConvert.SerializeObject(ex)}");
                throw;
            }
        }

        public async Task<CategoryDto> FindCategoryById(int id)
        {
            var category = await _ecomDbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category != null)
            {
                return new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description
                };
            }
            else
            {
                throw new NotFoundException("Category not found");
            }
        }

        public async Task<CategoryDto> UpdateCategory(int id, CategoryDto categoryDto)
        {
            var category = await _ecomDbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category != null)
            {
                var mapperUpdate = _mapper.Map<Category>(categoryDto);
                _ecomDbContext.Categories.Update(mapperUpdate);
                var check = _ecomDbContext.SaveChanges();
                if (check > 0)
                {
                    var updatedCategoryResponse = _mapper.Map<CategoryDto>(category);
                    return updatedCategoryResponse;
                }
                else
                {
                    throw new CustomException("Update process failed.");
                }
            }
            else
            {
                throw new NotFoundException("Category not found.");
            }
        }
        public async Task<CategoryDto> DeleteCategoryById(int id)
        {
            try
            {
                var category = await _ecomDbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (category != null)
                {
                    _ecomDbContext.Categories.Remove(category);
                    _ecomDbContext.SaveChanges();

                    var productDeletedResponse = _mapper.Map<CategoryDto>(category);
                    return productDeletedResponse;
                }
                else
                {
                    throw new CustomException("Category removal process failed.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Category Repository failed {JsonConvert.SerializeObject(ex)}");
                throw;
            }
        }




    }
}
