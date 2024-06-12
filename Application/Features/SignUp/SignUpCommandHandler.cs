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
        private readonly IIdentityService _identityService;

        public SignUpCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<SignUpModel> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
         

            var result = await _identityService.Register(request);

            return result;
        }
    }
}
