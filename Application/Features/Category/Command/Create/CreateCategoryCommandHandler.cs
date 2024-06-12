using Application.Contracts.Persistence;
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

namespace Application.Features.Category.Command.Create
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCategoryCommandHandler> _logger;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper, ILogger<CreateCategoryCommandHandler> logger)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CategoryDto> Handle(CreateCategoryCommand createCategoryCommand, CancellationToken cancellationToken)
        {
            try
            {
                var category = _mapper.Map<CategoryDto>(createCategoryCommand);
                var result = await _categoryRepository.CreateCategory(category);
                _logger.LogInformation($"Category created successfully ==> request {JsonConvert.SerializeObject(createCategoryCommand)}");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Category creation failed ==> request {JsonConvert.SerializeObject(createCategoryCommand)}, error {ex.Message}.");
                throw;
            }
        }
    }
}
