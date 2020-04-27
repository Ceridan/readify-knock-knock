using Microsoft.AspNetCore.Mvc;
using Readify.KnockKnock.Services;

namespace Readify.KnockKnock.Controllers
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
        public JsonResult GetReverseWords([FromQuery] string sentence)
        {
            var reversedWords = _wordsService.ReverseWordsInSentence(sentence);
            return new JsonResult(reversedWords);
        }
    }
}
