using Application.Contracts.Persistence;
using Application.Features.Product.Command.Create;
using AutoMapper;
using Domain.DTO;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Product.Command.Delete
{
    public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProductCommandHandler> _logger;

        public DeleteProductByIdCommandHandler(IProductRepository productRepository, IMapper mapper, ILogger<CreateProductCommandHandler> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ProductDto> Handle(DeleteProductByIdCommand deleteProductByIdCommand, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _productRepository.DeleteProductById(deleteProductByIdCommand.Id);
                _logger.LogInformation($"Deleted product successfully ==>  request  {JsonConvert.SerializeObject(deleteProductByIdCommand)}.");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Product delete function failed ==> request {JsonConvert.SerializeObject(deleteProductByIdCommand)}, error {ex.Message}.");
                throw;
            }
        }
    }
}
