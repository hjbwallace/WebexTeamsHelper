using System.Collections.Generic;
using System.Linq;

namespace WebexTeamsHelper
{
    public static class WebexTeamsMessageBuilderExtensions
    {
        public static WebexTeamsMessageBuilder AddLines(this WebexTeamsMessageBuilder builder, IEnumerable<string> lines)
        {
            return builder.AddLine(lines?.ToArray() ?? new string[0]);
        }

        public static WebexTeamsMessageBuilder AddBoldLines(this WebexTeamsMessageBuilder builder, params string[] value)
        {
            return builder.AddLines(value.Select(WebexTeamsFormatting.Bold));
        }

        public static WebexTeamsMessageBuilder AddItalicLines(this WebexTeamsMessageBuilder builder, params string[] value)
        {
            return builder.AddLines(value.Select(WebexTeamsFormatting.Italic));
        }

        public static WebexTeamsMessageBuilder AddInlineCode(this WebexTeamsMessageBuilder builder, params string[] value)
        {
            return builder.AddLines(value.Select(WebexTeamsFormatting.Inline));
        }
    }
}