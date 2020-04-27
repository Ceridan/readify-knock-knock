using System.Net;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace Readify.KnockKnock.IntegrationTests
{
    public class ReverseWordsTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public ReverseWordsTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("cat", "tac")]
        [InlineData("cat and dog", "tac dna god")]
        [InlineData("two  spaces", "owt  secaps")]
        [InlineData(" leading space", " gnidael ecaps")]
        [InlineData("trailing space ", "gniliart ecaps ")]
        [InlineData("Capital", "latipaC")]
        [InlineData("Bang!", "!gnaB")]
        [InlineData("¿Qué?", "?éuQ¿")]
        [InlineData("Привет, мир!", ",тевирП !рим")]
        [InlineData("detartrated kayak detartrated", "detartrated kayak detartrated")]
        [InlineData("This is a snark: ⸮", "sihT si a :krans ⸮")]
        [InlineData("  S  P  A  C  E  Y  ", "  S  P  A  C  E  Y  ")]
        [InlineData("!B!A!N!G!S!", "!S!G!N!A!B!")]
        [InlineData("P!u@n#c$t%u^a&t*i(o)n", "n)o(i*t&a^u%t$c#n@u!P")]
        public async Task GivenGetReverseWords_WhenPassAnySentence_ShouldReturnSuccessWithCorrectResult(
            string sentence,
            string expected)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"/api/ReverseWords?sentence={HttpUtility.UrlEncode(sentence)}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<string>(content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(expected, result);
        }
    }
}
