using Application.Contracts.Infrastructure;
using Application.Features.Category.Command.Create;
using Application.Features.Category.Command.Delete;
using Application.Features.Category.Command.Update;
using Application.Features.Category.Query;
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
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ApiController
    {

        [HttpPost("CreateCategory")]
        public async Task<ActionResult<ResponseModel>> CreateCategory([FromBody] CreateCategoryCommand createCategoryCommand)
        {
            var result = await Mediator.Send(createCategoryCommand);
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

        [HttpGet("FindCategory")]
        public async Task<ActionResult<ResponseModel>> FindCategory([FromQuery] FindCategoryByIdQuery findCategoryById)
        {
            var result = await Mediator.Send(findCategoryById);
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

        [HttpPut("UpdateCategory")]
        public async Task<ActionResult<ResponseModel>> UpdateCategory([FromBody] UpdateCategoryCommand updateCategoryCommand)
        {
            var result = await Mediator.Send(updateCategoryCommand);
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

        [HttpDelete("DeleteCategoryById")]
        public async Task<ActionResult<ResponseModel>> DeleteCategoryById([FromBody] DeleteCategoryByIdCommand deleteCategoryByIdCommand)
        {
            var result = await Mediator.Send(deleteCategoryByIdCommand);
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
    }
}
