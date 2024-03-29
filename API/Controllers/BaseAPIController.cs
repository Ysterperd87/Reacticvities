using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseAPIController : ControllerBase
    {
        private IMediator mediator;
        protected IMediator Mediator => mediator ?? HttpContext.RequestServices.GetService<IMediator>();

        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result.isSuccess && result.Value != null)
                return Ok(result.Value);
            if (result.isSuccess && result.Value == null)
                return NotFound();
            return BadRequest(result.Error);
        }
    }
}