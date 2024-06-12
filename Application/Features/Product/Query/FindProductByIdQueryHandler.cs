using Application.Contracts.Persistence;
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

namespace Application.Features.Product.Query
{
    public class FindProductByIdQueryHandler : IRequestHandler<FindProductByIdQuery, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<FindProductByIdQueryHandler> _logger;

        public FindProductByIdQueryHandler(IProductRepository productRepository, ILogger<FindProductByIdQueryHandler> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ProductDto> Handle(FindProductByIdQuery findProductByIdQuery, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _productRepository.FindProductById(findProductByIdQuery.Id);
                _logger.LogInformation($"Find product successful ==>  request  {JsonConvert.SerializeObject(findProductByIdQuery)}.");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Find product unsuccessful  ==>  request  {JsonConvert.SerializeObject(findProductByIdQuery)}.");
                throw ex;
            }

        }
    }
}
