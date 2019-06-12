using System.Linq;

namespace Readify.KnockKnock.Api.Services
{
    public class WordsService : IWordsService
    {
        public string ReverseWordsInSentence(string sentence)
        {
            var reversedSentence = new string(sentence.Reverse().ToArray());
            var words = reversedSentence.Split();
            return string.Join(' ', words.Reverse());
        }
    }
}
