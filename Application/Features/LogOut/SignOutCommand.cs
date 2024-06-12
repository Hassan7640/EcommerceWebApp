using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LogOut
{
    public class SignOutCommand : IRequest<bool>
    {
        string Email { get; set; }
    }
}
