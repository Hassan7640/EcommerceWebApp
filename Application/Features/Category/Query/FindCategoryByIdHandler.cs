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

namespace Application.Features.Category.Query
{
    public class FindCategoryByIdHandler : IRequestHandler<FindCategoryByIdQuery, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<FindCategoryByIdHandler> _logger;

        public FindCategoryByIdHandler(ICategoryRepository categoryRepository, ILogger<FindCategoryByIdHandler> logger)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CategoryDto> Handle(FindCategoryByIdQuery findCategoryByIdQuery, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _categoryRepository.FindCategoryById(findCategoryByIdQuery.Id);
                _logger.LogInformation($"Find category successful ==>  request  {JsonConvert.SerializeObject(findCategoryByIdQuery)}.");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Find category unsuccessful  ==>  request  {JsonConvert.SerializeObject(findCategoryByIdQuery)}.");
                throw ex;
            }

        }
    }
}
