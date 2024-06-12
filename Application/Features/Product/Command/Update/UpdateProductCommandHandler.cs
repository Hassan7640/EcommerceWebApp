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

namespace Application.Features.Product.Command.Update
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProductCommandHandler> _logger;

        public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper, ILogger<CreateProductCommandHandler> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ProductDto> Handle(UpdateProductCommand updateProductCommand, CancellationToken cancellationToken)
        {
            try
            {
                var updateProduct = _mapper.Map<ProductDto>(updateProductCommand);

                var result = await _productRepository.UpdateProduct(updateProductCommand.Id, updateProduct);
                _logger.LogInformation($"Product update was successfull ==>  request  {JsonConvert.SerializeObject(updateProductCommand)}.");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Product update function failed ==> request {JsonConvert.SerializeObject(updateProductCommand)}, error {ex.Message}.");
                throw;
            }
        }
    }
}
