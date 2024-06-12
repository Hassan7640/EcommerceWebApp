using Application.Contracts.Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.LogOut
{
    public class SignOutCommandHandler : IRequestHandler<SignOutCommand, bool>
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger _logger;
        public SignOutCommandHandler(IIdentityService identityService, ILogger<SignOutCommandHandler> logger)
        {
            _identityService = identityService;
            _logger = logger;
        }
        public async Task<bool> Handle(SignOutCommand request, CancellationToken cancellationToken)
        {
            try
            {
               return await _identityService.LogOut();
            }catch(Exception ex)
            {
                _logger.LogError($"SignOutCommandHandler failed, {JsonConvert.SerializeObject(ex)}");
                throw;
            }
        }
    }
}
