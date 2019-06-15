using System;
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
        public async Task<IActionResult> GetFibonacci([FromQuery] long n)
        {
            try
            {
                // Using Task.Run here because of potentially huge CPU-bound operation
                var nthFibonacciElement = await Task.Run(() => _fibonacciService.GetSignedNthFibonacciElement(n));
                return Ok(nthFibonacciElement);
            }
            catch (OverflowException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
