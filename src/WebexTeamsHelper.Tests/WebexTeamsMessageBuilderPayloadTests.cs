using System.Collections.Generic;
using Xunit;

namespace WebexTeamsHelper.Tests
{
    public class WebexTeamsMessageBuilderPayloadTests : PayloadTestBase
    {

        [Fact]
        public void AddMultipleLines()
        {
            AssertPayload(x => x.
                AddLine("Some line", "Another line"),
                $"Some line{LineBreak}Another line");
        }

        [Fact]
        public void AddOrderedListWithoutPrompt()
        {
            AssertPayload(x => x.
                AddOrderedList(null, "Item 1", "", "Item 2"),
                $"1. Item 1{LineBreak}1. Item 2{LineBreak}{LineBreak}");
        }

        [Fact]
        public void AddOrderedListWithPrompt()
        {
            AssertPayload(x => x.
                AddOrderedList("Prompt", "Item 1", "", "Item 2"),
                $"Prompt{LineBreak}1. Item 1{LineBreak}1. Item 2{LineBreak}{LineBreak}");
        }

        [Fact]
        public void AddUnorderedListWithoutPrompt()
        {
            AssertPayload(x => x.
                AddUnorderedList(null, "Item 1", "", "Item 2"),
                $"- Item 1{LineBreak}- Item 2{LineBreak}{LineBreak}");
        }

        [Fact]
        public void AddUnorderedListWithPrompt()
        {
            AssertPayload(x => x.
                AddUnorderedList("Prompt", "Item 1", "", "Item 2"),
                $"Prompt{LineBreak}- Item 1{LineBreak}- Item 2{LineBreak}{LineBreak}");
        }

        [Fact]
        public void AddLinkWithoutDisplay()
        {
            var link = "https://www.google.com/";
            AssertPayload(x => x.AddLink(link), link);
        }

        [Fact]
        public void AddLinkWithDisplay()
        {
            var link = "https://www.google.com/";
            var display = "Google";
            var expected = $"[{display}]({link})";

            AssertPayload(x => x.AddLink(link, display), expected);
        }

        [Fact]
        public void AddMentionAll()
        {
            AssertPayload(x => x.AddMentionAll(), $"<@all>");
        }

        [Fact]
        public void AddMentionWithoutDisplayName()
        {
            var email = "example@test.com";
            var expected = $"<@personEmail:{email}|{email}>";

            AssertPayload(x => x.AddMention(email), expected);
        }

        [Fact]
        public void AddMentionWithDisplayName()
        {
            var email = "example@test.com";
            var displayName = "Example";
            var expected = $"<@personEmail:{email}|{displayName}>";

            AssertPayload(x => x.AddMention(email, displayName), expected);
        }

        [Fact]
        public void AddMentionMany()
        {
            var dictionary = new Dictionary<string, string>
            {
                ["example1@test.com"] = "Example",
                ["example2@test.com"] = null
            };

            var expected = $"<@personEmail:example1@test.com|Example>, <@personEmail:example2@test.com|example2@test.com>";

            AssertPayload(x => x.AddMentionMany(dictionary), expected);
        }

        [Theory]
        [InlineData("Some quote")]
        public void AddQuote(string quote)
        {
            AssertPayload(x => x.AddQuote(quote), $"> {quote}");
        }

        [Theory]
        [InlineData("Some bold text")]
        public void AddBoldLines(string line)
        {
            AssertPayload(x => x.AddBoldLines(line), $"**{line}**");
        }

        [Theory]
        [InlineData("Some italic text")]
        public void AddItalicLines(string line)
        {
            AssertPayload(x => x.AddItalicLines(line), $"*{line}*");
        }

        [Theory]
        [InlineData("Some inline text")]
        public void AddInlineCode(string line)
        {
            AssertPayload(x => x.AddInlineCode(line), $"`{line}`");
        }

        [Theory]
        [InlineData("Heading")]
        public void AddHeadingWithoutExtraLine(string heading)
        {
            AssertPayload(x => x.AddHeading(heading, false), $"**{heading}**");
        }

        [Theory]
        [InlineData("Heading")]
        public void AddHeadingWithExtraLine(string heading)
        {
            AssertPayload(x => x.AddHeading(heading, true), $"**{heading}**{LineBreak}");
        }
    }
}