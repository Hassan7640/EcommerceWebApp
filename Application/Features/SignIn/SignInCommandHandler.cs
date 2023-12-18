using Application.Contracts.Infrastructure;
using Application.Exceptions;
using Application.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Application.Contracts.Infrastructure.Constants;

namespace Application.Features.SignIn
{
    public class SignInCommandHandler : IRequestHandler<SignInCommand, SignInResponse>
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger<SignInCommandHandler> _logger;

        public SignInCommandHandler(ILogger<SignInCommandHandler> logger, IIdentityService identityService)
        {
            _identityService = identityService ?? throw new ArgumentNullException(nameof(logger));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<SignInResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var appUser = await _identityService.Authenticate(request);
                if (appUser != null)
                {
                    var user = await _identityService.GenerateToken(appUser);
                    user.RequestStatus = true;
                    user.ResponseCode = ResponseCode.Succesfull;
                    user.Message = "You have succesfully Signed In";
                    user.Profile = new UserModel
                    {
                        Email = appUser.Email,
                        FirstName = appUser.FirstName,
                        LastName = appUser.LastName,
                        Id = appUser.Id,
                        Status = appUser.UserStatus
                    };
                    return user;
                }
                else
                {
                    throw new CustomException("Invalid Credentials");
                }
            }catch(Exception e)
            {
                _logger.LogError($"Sign in threw an error, {JsonConvert.SerializeObject(e)}");
                throw;
            }
        }
    }
}
