using Xunit;

namespace WebexTeamsHelper.Tests
{
    public class WebexTeamsMessageBuilderComplexPayloadTests : PayloadTestBase
    {
        [Fact]
        public void AddLineWithMultipleFormattingOptions()
        {
            var boldText = WebexTeamsFormatting.Bold("Bold");
            var italicText = WebexTeamsFormatting.Italic("Italic");
            var inlineText = WebexTeamsFormatting.Inline("Inline");

            var expected = "Line with **Bold**, *Italic* and `Inline`.";

            AssertPayload(x => x.
                AddLines($"Line with {boldText}, {italicText} and {inlineText}."),
                expected);
        }
    }
}