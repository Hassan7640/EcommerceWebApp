using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence
{
    public interface IProductRepository
    {
        Task<ProductDto> CreateProduct(ProductDto productDto);
        Task<ProductDto> FindProductById(int id);
        Task<ProductDto> UpdateProduct(int id, ProductDto productDto);
        Task<ProductDto> DeleteProductById(int id);
       

    }
}
