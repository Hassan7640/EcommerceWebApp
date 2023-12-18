using Application.Contracts.Infrastructure;
using Application.Exceptions;
using Application.Models;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.SignUp
{
    public class SignUpCommandHandler : IRequestHandler<SignUpCommand,SignUpModel>
    {
        private readonly ILogger<SignUpCommandHandler> _logger;
        private readonly IIdentityService _identityService;

        public SignUpCommandHandler(ILogger<SignUpCommandHandler> logger, IIdentityService identityService)
        {
            _logger = logger;
            _identityService = identityService;
        }
        public async Task<SignUpModel> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            SignUpCommandValidation signUpValidation = new SignUpCommandValidation();
            var validationResult = await signUpValidation.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                _logger.LogInformation($"validation for request {JsonConvert.SerializeObject(request)} failed");
                //throw new LocalValidationException(validationResult.Errors);
            }

            var result = await _identityService.Register(request);

            return result;
        }
    }
}
