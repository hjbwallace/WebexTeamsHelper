using Xunit;

namespace WebexTeamsHelper.Tests
{
    public class WebexTeamsMessageBuilderValidationTests : ValidationTestBase
    {
        private readonly WebexTeamsMessageBuilder _builder = new WebexTeamsMessageBuilder();

        [Theory]
        [StringValidation]
        public void ThrowsWhenLinePartIsNullOrEmpty(string linePart)
        {
            ThrowsArgumentException(() => _builder.AddLine(linePart));
        }

        [Theory]
        [StringValidation]
        public void ThrowsWhenCodeBlockValueIsNullOrEmpty(string codeBlockLine)
        {
            ThrowsArgumentException(() => _builder.AddCodeBlock(codeBlockLine));
        }

        [Fact]
        public void ThrowsWhenAllCodeBlockValuesAreEmpty()
        {
            ThrowsArgumentException(() => _builder.AddCodeBlock(EmptyStrings));
        }

        [Fact]
        public void ThrowsWhenNoOrderedListValues()
        {
            ThrowsArgumentException(() => _builder.AddOrderedList("Prompt"));
        }

        [Theory]
        [StringValidation]
        public void ThrowsWhenOrderedListValueIsNullOrEmpty(string listItem)
        {
            ThrowsArgumentException(() => _builder.AddOrderedList("Prompt", listItem));
        }

        [Fact]
        public void ThrowsWhenAllOrderedListValuesAreEmpty()
        {
            ThrowsArgumentException(() => _builder.AddOrderedList("Prompt", EmptyStrings));
        }

        [Fact]
        public void ThrowsWhenNoUnorderedListValues()
        {
            ThrowsArgumentException(() => _builder.AddUnorderedList("Prompt"));
        }

        [Theory]
        [StringValidation]
        public void ThrowsWhenUnorderedListValueIsNullOrEmpty(string listItem)
        {
            ThrowsArgumentException(() => _builder.AddUnorderedList("Prompt", listItem));
        }

        [Fact]
        public void ThrowsWhenAllUnorderedListValuesAreEmpty()
        {
            ThrowsArgumentException(() => _builder.AddUnorderedList("Prompt", EmptyStrings));
        }

        [Theory]
        [StringValidation]
        public void ThrowsWhenHeadingIsNullOrEmpty(string heading)
        {
            ThrowsArgumentException(() => _builder.AddHeading(heading));
        }

        [Theory]
        [StringValidation]
        public void ThrowsWhenLinkIsNullOrEmpty(string link)
        {
            ThrowsArgumentException(() => _builder.AddLink(link));
        }

        [Theory]
        [StringValidation]
        public void ThrowsWhenMentionEmailIsNullOrEmpty(string userName)
        {
            ThrowsArgumentException(() => _builder.AddMention(userName));
        }

        [Theory]
        [StringValidation]
        public void ThrowsWhenQuoteIsNullOrEmpty(string quote)
        {
            ThrowsArgumentException(() => _builder.AddQuote(quote));
        }
    }
}