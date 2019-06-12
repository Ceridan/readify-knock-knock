using Microsoft.AspNetCore.Mvc;
using Readify.KnockKnock.Api.Services;

namespace Readify.KnockKnock.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TriangleTypeController : ControllerBase
    {
        private readonly ITriangleService _triangleService;

        public TriangleTypeController(ITriangleService triangleService)
        {
            _triangleService = triangleService;
        }

        [HttpGet]
        public ActionResult<string> GetTriangleType(int a, int b, int c)
        {
            var triangleType = _triangleService.DetermineTriangleType(a, b, c);
            return Ok(triangleType);
        }
    }
}
