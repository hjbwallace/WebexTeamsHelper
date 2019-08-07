using System;
using System.Collections.Generic;
using System.Linq;

namespace WebexTeamsHelper
{
    public class WebexTeamsMessageBuilder
    {
        private readonly List<string> _lines = new List<string>();

        public WebexTeamsMessageBuilder AddUnorderedList(string prompt, params string[] items)
        {
            return AddList(prompt, items, (x) => $"- {x}");
        }

        public WebexTeamsMessageBuilder AddOrderedList(string prompt, params string[] items)
        {
            return AddList(prompt, items, (x) => $"1. {x}");
        }

        public WebexTeamsMessageBuilder AddQuote(string quote)
        {
            ParameterValidator.IsPopulated(quote, "Quote");
            return AddLines(WebexTeamsFormatting.Quote(quote));
        }

        public WebexTeamsMessageBuilder AddHeading(string heading, bool additionalLineBreak = true)
        {
            ParameterValidator.IsPopulated(heading, "Heading");

            var headingText = WebexTeamsFormatting.Bold(heading) + (additionalLineBreak ? WebexTeamsFormatting.LineBreak : "");
            return AddLines(headingText);
        }

        public WebexTeamsMessageBuilder AddLines(params string[] lines)
        {
            ParameterValidator.AreValuesPopulated(lines, "Lines");

            _lines.AddRange(lines);
            return this;
        }

        public WebexTeamsMessageBuilder AddLine(params string[] lineParts)
        {
            ParameterValidator.AreValuesPopulated(lineParts, "Line parts");

            var line = string.Join(" ", lineParts.Where(x => !string.IsNullOrWhiteSpace(x)));
            return AddLines(line);
        }

        public WebexTeamsMessageBuilder AddCodeBlock(params string[] codeLines)
        {
            if (codeLines?.Any() != true)
                return this;

            ParameterValidator.AreValuesPopulated(codeLines, "Code block");

            AddLines("```");
            AddLines(codeLines);
            AddLines("```");
            return this;
        }

        public WebexTeamsMessageBuilder AddMention(string userEmail, string nickName = null)
        {
            ParameterValidator.IsPopulated(userEmail, "Mention email");

            var mentionLine = GenerateUserMentionText(userEmail, nickName);
            return AddLines(mentionLine);
        }

        public WebexTeamsMessageBuilder AddMentionAll()
        {
            return AddLines($"<@all>");
        }

        public WebexTeamsMessageBuilder AddMentionMany(IDictionary<string, string> users)
        {
            ParameterValidator.AreValuesPopulated(users.Values, "Mention users");

            var mentions = users.Select(x => GenerateUserMentionText(x.Key, x.Value));
            var line = string.Join(", ", mentions);
            return AddLines(line);
        }

        public WebexTeamsMessageBuilder AddLink(string link, string display = null)
        {
            var linkText = GenerateLinkText(link, display);
            return AddLines(linkText);
        }

        public WebexTeamsMessage Build()
        {
            ParameterValidator.AreValuesPopulated(_lines, "Lines");

            var markdown = string.Join(WebexTeamsFormatting.LineBreak, _lines);
            return new WebexTeamsMessage(markdown);
        }

        private WebexTeamsMessageBuilder AddList(string prompt, string[] items, Func<string, string> itemSelector)
        {
            ParameterValidator.AreValuesPopulated(items, "Items");

            if (!string.IsNullOrWhiteSpace(prompt))
                AddLines(prompt);

            var listItems = items
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(itemSelector);

            return AddLines(string.Join(WebexTeamsFormatting.LineBreak, listItems) + WebexTeamsFormatting.LineBreak + WebexTeamsFormatting.LineBreak);
        }

        private string GenerateUserMentionText(string userName, string nickName)
        {
            ParameterValidator.IsPopulated(userName, "User name");

            var displayName = string.IsNullOrWhiteSpace(nickName) ? userName : nickName;
            return $"<@personEmail:{userName}|{displayName}>";
        }

        private string GenerateLinkText(string link, string display)
        {
            ParameterValidator.IsPopulated(link, "Link");

            if (string.IsNullOrWhiteSpace(display))
                return link;

            return $"[{display}]({link})";
        }
    }
}