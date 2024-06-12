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
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(EcomDbContext ecomDbContext, IMapper mapper, ILogger<ProductRepository> logger) : base(ecomDbContext)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ProductDto> CreateProduct(ProductDto productDto)
        {
            try
            {
                var product = await _ecomDbContext.Products.FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == productDto.Name.ToLower().Trim());
                if (product == null)
                {
                    var productMapper = _mapper.Map<Product>(productDto);
                    _ecomDbContext.Products.Add(productMapper);
                    int saved = await _ecomDbContext.SaveChangesAsync();
                    if (saved > 0)
                    {
                        var currentProduct = await _ecomDbContext.Products.FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == productDto.Name.ToLower().Trim());
                        var productResponse = _mapper.Map<ProductDto>(currentProduct);
                        return productResponse;
                    }
                    else
                    {
                        throw new CustomException("Product could not be saved. Please try again.");
                    }
                }
                else
                {
                    throw new AlreadyExistException("Product already exists.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Product Repository failed {JsonConvert.SerializeObject(ex)}");
                throw;
            }
        }

        public async Task<ProductDto> FindProductById(int id)
        {
            var product = await _ecomDbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product != null)
            {

                return new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Category = product.Category
                };
            }
            else
            {
                throw new NotFoundException("Product not found");
            }
        }

        public async Task<ProductDto> UpdateProduct(int id, ProductDto productDto)
        {
            var product = await _ecomDbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product != null)
            {
                var mapUpdate = _mapper.Map<Product>(productDto);
                _ecomDbContext.Products.Update(mapUpdate);
                var check = _ecomDbContext.SaveChanges();
                if (check > 0)
                {
                    var updatedProductResponse = _mapper.Map<ProductDto>(product);
                    return updatedProductResponse;
                }
                else
                {
                    throw new CustomException("Update process failed.");
                }
            }
            else
            {
                throw new NotFoundException("Product not found.");
            }
        }

        public async Task<ProductDto> DeleteProductById(int id)
        {
            try
            {
                var product = await _ecomDbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                {
                    _ecomDbContext.Products.Remove(product);
                    _ecomDbContext.SaveChanges();

                    var deletedProductResponse = _mapper.Map<ProductDto>(product);
                    return deletedProductResponse;
                }
                else
                {
                    throw new CustomException("Product removal process failed.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Product Repository failed {JsonConvert.SerializeObject(ex)}");
                throw;
            }
        }


    }
}
