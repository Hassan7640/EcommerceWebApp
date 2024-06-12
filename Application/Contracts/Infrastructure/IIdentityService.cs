using Application.Features.SignIn;
using Application.Features.SignUp;
using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Infrastructure
{
    public interface IIdentityService
    {
        Task<SignUpModel> Register(SignUpCommand signUpCommand);
        Task<ApplicationUser> Authenticate(SignInCommand signInCommand);

        Task<SignInResponse> GenerateToken(ApplicationUser user);
        Task<bool> LogOut();
    }
}
