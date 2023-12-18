using Application.Features.SignIn;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Application.Contracts.Infrastructure.Constants;

namespace EcommerceWebApp.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ActionResult<ResponseModel>> Authenticate ([FromBody] SignInCommand signInCommand)
        {
            var result = await _mediator.Send(signInCommand);
            if(result != null)
            {
                var response = new ResponseModel
                {
                    Data = result,
                    ResponseCode = ResponseCode.Succesfull,
                    Status = true,
                    Message = "Success"
                };
                return Ok(response);
            }
            return BadRequest();
        }


    }
}
