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

namespace Application.Features.Category.Command.Update
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCategoryCommandHandler> _logger;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper, ILogger<UpdateCategoryCommandHandler> logger)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CategoryDto> Handle(UpdateCategoryCommand updateCategoryCommand, CancellationToken cancellationToken)
        {
            try
            {
                var updateCategory = _mapper.Map<CategoryDto>(updateCategoryCommand);

                var result = await _categoryRepository.UpdateCategory(updateCategoryCommand.Id, updateCategory);
                _logger.LogInformation($"Category update was successfull ==>  request  {JsonConvert.SerializeObject(updateCategoryCommand)}.");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Category update function failed ==> request {JsonConvert.SerializeObject(updateCategoryCommand)}, error {ex.Message}.");
                throw;
            }
        }
    }
}
