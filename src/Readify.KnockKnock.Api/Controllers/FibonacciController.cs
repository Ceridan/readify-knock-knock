using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Readify.KnockKnock.Api.Services;

namespace Readify.KnockKnock.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FibonacciController : ControllerBase
    {
        private readonly IFibonacciService _fibonacciService;

        public FibonacciController(IFibonacciService fibonacciService)
        {
            _fibonacciService = fibonacciService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFibonacci(long n)
        {
            var nthFibonacciElement = await Task.Run(() => _fibonacciService.GetSignedNthFibonacciElement(n));
            return Ok(nthFibonacciElement);
        }
    }
}
