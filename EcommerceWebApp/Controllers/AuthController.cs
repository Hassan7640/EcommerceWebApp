using Application.Contracts.Infrastructure;
using Application.Features;
using Application.Features.SignIn;
using Application.Features.SignUp;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Application.Contracts.Infrastructure.Constants;

namespace EcommerceWebApp.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ApiController
    {
      
        [HttpPost("Login")]
        public async Task<ActionResult<ResponseModel>> Authenticate([FromBody] SignInCommand signInCommand)
        {
            var result = await Mediator.Send(signInCommand);
            if (result != null)
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
            return BadRequest(Constants.BadResponse());
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ResponseModel>> RegisterUser([FromBody] SignUpCommand signUpCommand)
        {
            var result = await Mediator.Send(signUpCommand);
            if (result != null)
            {
                string message;
                if (result.isEmailSent)
                {
                    message = "Success";
                }
                else
                {
                    message = "Your account has been created but email not received";
                }
                var response = new ResponseModel
                {
                    Data = result,
                    ResponseCode = ResponseCode.Succesfull,
                    Status = true,
                    Message = message
                };
                return Ok(response);
            }
            return BadRequest(Constants.BadResponse());
        }

        [HttpPost("Logout")]
        public async Task<ActionResult<ResponseModel>> Logout([FromBody] SignOutCommand signOutCommand)
        {
            var result = await Mediator.Send(signOutCommand);
            if (result)
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
            return BadRequest(Constants.BadResponse());
        }


    }

}

