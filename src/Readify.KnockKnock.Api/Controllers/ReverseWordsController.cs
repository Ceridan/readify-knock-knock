using Microsoft.AspNetCore.Mvc;
using Readify.KnockKnock.Api.Services;

namespace Readify.KnockKnock.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReverseWordsController : ControllerBase
    {
        private readonly IWordsService _wordsService;

        public ReverseWordsController(IWordsService wordsService)
        {
            _wordsService = wordsService;
        }

        [HttpGet]
        public ActionResult GetReverseWords([FromQuery] string sentence)
        {
            var reversedWords = _wordsService.ReverseWordsInSentence(sentence);
            return Ok(reversedWords);
        }
    }
}
