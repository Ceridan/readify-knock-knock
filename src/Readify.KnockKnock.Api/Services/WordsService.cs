using System.Linq;

namespace Readify.KnockKnock.Services
{
    public class WordsService : IWordsService
    {
        public string ReverseWordsInSentence(string sentence)
        {
            if (string.IsNullOrEmpty(sentence))
            {
                return string.Empty;
            }

            var reversedSentence = new string(sentence.Reverse().ToArray());
            var words = reversedSentence.Split();
            return string.Join(' ', words.Reverse());
        }
    }
}
