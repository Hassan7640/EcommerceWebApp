using Application.Contracts.Infrastructure;
using Application.Features.Product.Command;
using Application.Features.Product.Command.Delete;
using Application.Features.Product.Query;
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
    public class ProductController : ApiController
    {

        [HttpPost("CreateProduct")]
        public async Task<ActionResult<ResponseModel>> CreateProduct([FromBody] CreateProductCommand createProductCommand)
        {
            var result = await Mediator.Send(createProductCommand);
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

        [HttpGet("FindProductById")]
        public async Task<ActionResult<ResponseModel>> FindProductById([FromQuery] FindProductByIdQuery findProductByIdQuery)
        {
            var result = await Mediator.Send(findProductByIdQuery);
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

        [HttpPut("UpdateProduct")]
        public async Task<ActionResult<ResponseModel>> UpdateProduct([FromBody] UpdateProductCommand updateProductCommand)
        {
            var result = await Mediator.Send(updateProductCommand);
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

        [HttpPost("DeleteProductById")]
        public async Task<ActionResult<ResponseModel>> DeleteProductById([FromBody] DeleteProductByIdCommand deleteProductCommand)
        {
            var result = await Mediator.Send(deleteProductCommand);
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
