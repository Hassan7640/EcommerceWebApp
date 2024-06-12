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

namespace Application.Features.Category.Command.Delete
{
    public class DeleteCategoryByIdCommandHandler : IRequestHandler<DeleteCategoryByIdCommand, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<DeleteCategoryByIdCommandHandler> _logger;

        public DeleteCategoryByIdCommandHandler(ICategoryRepository categoryRepository, ILogger<DeleteCategoryByIdCommandHandler> logger)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CategoryDto> Handle(DeleteCategoryByIdCommand deleteCategoryByIdCommand, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _categoryRepository.DeleteCategoryById(deleteCategoryByIdCommand.Id);
                _logger.LogInformation($"Deleted category successfully ==>  request  {JsonConvert.SerializeObject(deleteCategoryByIdCommand)}.");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Category delete function failed ==> request {JsonConvert.SerializeObject(deleteCategoryByIdCommand)}, error {ex.Message}.");
                throw;
            }
        }
    }
}
