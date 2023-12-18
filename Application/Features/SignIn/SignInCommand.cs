using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SignIn
{
   public class SignInCommand : IRequest<SignInResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
