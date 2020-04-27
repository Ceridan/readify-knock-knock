using System;
using Microsoft.AspNetCore.Mvc;

namespace Readify.KnockKnock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetToken()
        {
            var token = Environment.GetEnvironmentVariable("READIFY_TOKEN") ?? Guid.Empty.ToString();
            return Ok(token);
        }
    }
}
