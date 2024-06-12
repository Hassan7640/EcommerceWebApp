using Application.Contracts.Persistence;
using AutoMapper;
using Domain.DTO;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Product.Command.Create
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProductCommandHandler> _logger;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper, ILogger<CreateProductCommandHandler> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ProductDto> Handle(CreateProductCommand createProductCommand, CancellationToken cancellationToken)
        {
            try
            {
                CreateProductCommandValidation createProductCommandValidation = new CreateProductCommandValidation();
                var validationResult = await createProductCommandValidation.ValidateAsync(createProductCommand);
                if (!validationResult.IsValid)
                {
                    _logger.LogInformation($"Validation for request {JsonConvert.SerializeObject(createProductCommand)} failed.");
                    throw new ValidationException(validationResult.Errors);
                }

                var product = _mapper.Map<ProductDto>(createProductCommand);
                var result = await _productRepository.CreateProduct(product);
                _logger.LogInformation($"Product creation was successfull ==>  request  {JsonConvert.SerializeObject(createProductCommand)}.");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Product creation failed ==> request {JsonConvert.SerializeObject(createProductCommand)}, error {ex.Message}.");
                throw;
            }

        }
    }
}
