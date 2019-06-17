using System.Net;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Readify.KnockKnock.IntegrationTests.DSL;
using Xunit;

namespace Readify.KnockKnock.IntegrationTests
{
    public class ReverseWordsTests
    {
        private const string EndpointUrl = "/api/ReverseWords";
        private readonly TestServer _server;

        public ReverseWordsTests()
        {
            _server = TestServerFactory.CreateTestServer();
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("cat", "tac")]
        [InlineData("cat and dog", "tac dna god")]
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
            var client = _server.CreateClient();

            var response = await client.GetAsync($"{EndpointUrl}?sentence={HttpUtility.UrlEncode(sentence)}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<string>(content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(expected, result);
        }
    }
}
