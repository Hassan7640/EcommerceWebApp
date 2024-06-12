using Application.Contracts.Infrastructure;
using Application.Exceptions;
using Application.Features.SignIn;
using Application.Features.SignUp;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
  public  class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        private readonly ILogger<IdentityService> _logger;
        public IdentityService(IMapper mapper, ILogger<IdentityService> logger, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApplicationUser> Authenticate(SignInCommand signInCommand)
        {
            var appUser = await _userManager.Users.FirstOrDefaultAsync(p => p.UserName == signInCommand.UserName || p.Email == signInCommand.UserName);
            if(appUser != null) {

                var user = await _signInManager.CheckPasswordSignInAsync(appUser, signInCommand.Password, false);
                if (!user.Succeeded)
                {
                    await _userManager.AccessFailedAsync(appUser);
                    throw new Exception("Invalid username or password");
                }
                return appUser;
            }
            else
            {
                throw new Exception("User not found");
            }
        }

        public async Task<SignUpModel> Register(SignUpCommand signUpCommand)
        {
            try
            {
                var user = _mapper.Map<ApplicationUser>(signUpCommand);
                var userIdentity = await _userManager.CreateAsync(user, signUpCommand.Password);
                if (userIdentity.Succeeded)
                {
                    var newCreatedUser = await _userManager.FindByNameAsync(user.UserName);

                    var result = _mapper.Map<SignUpModel>(signUpCommand);
                    result.Id = newCreatedUser.Id;
                    _logger.LogInformation($"User {JsonConvert.SerializeObject(user)} is successully created.");
                    return result;
                }
                else
                {
                    _logger.LogInformation($"User {JsonConvert.SerializeObject(user)} is not successully created.");
                    //throw;
                    throw new LocalValidationException(userIdentity.Errors);
                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"User {JsonConvert.SerializeObject(ex)} creation failed.");
                throw;
            }
        }

        public async Task<SignInResponse> GenerateToken(ApplicationUser user)
        {

            var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }

                ),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
            string token = jwtSecurityTokenHandler.WriteToken(securityToken);
            return new SignInResponse
            {
                AccessToken = token,
                ExpiryIn = tokenDescriptor.Expires,
                RefreshToken = "",
                TokenType = "Bearer"
            };

        }

        public async Task<bool> LogOut()
        {
            await _signInManager.SignOutAsync();
            return true;
        }
    }
}
