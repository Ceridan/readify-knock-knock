using System;
using Microsoft.AspNetCore.Mvc;

namespace Readify.KnockKnock.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> GetToken()
        {
            var token = Environment.GetEnvironmentVariable("READIFY_TOKEN") ?? Guid.Empty.ToString();
            return Ok(token);
        }
    }
}
