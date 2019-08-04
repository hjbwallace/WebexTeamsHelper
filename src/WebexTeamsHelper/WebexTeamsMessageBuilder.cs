using Newtonsoft.Json;
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
            return AddLine("> " + quote);
        }

        public WebexTeamsMessageBuilder AddHeading(string heading, bool additionalLineBreak = true)
        {
            ParameterValidator.IsPopulated(heading, "Heading");

            _lines.Add(WebexTeamsFormatting.Bold(heading));

            if (additionalLineBreak)
                _lines.Add(WebexTeamsFormatting.LineBreak);

            return this;
        }

        public WebexTeamsMessageBuilder AddLine(params string[] messages)
        {
            _lines.AddRange(messages);
            return this;
        }

        public WebexTeamsMessageBuilder AddCodeBlock(params string[] codeLines)
        {
            if (codeLines?.Any() != true)
                return this;

            ParameterValidator.AreValuesPopulated(codeLines, "Code block");

            _lines.Add("```");
            _lines.AddRange(codeLines);
            _lines.Add("```");
            return this;
        }

        public WebexTeamsMessageBuilder AddMention(string userEmail, string nickName = null)
        {
            ParameterValidator.IsPopulated(userEmail, "Mention email");

            var mentionLine = GenerateUserMentionLine(userEmail, nickName);
            _lines.Add(mentionLine);
            return this;
        }

        public WebexTeamsMessageBuilder AddMentionAll()
        {
            _lines.Add($"<@all>");
            return this;
        }

        public WebexTeamsMessageBuilder AddMentionMany(IDictionary<string, string> users)
        {
            ParameterValidator.AreValuesPopulated(users.Values, "Mention users");

            var mentions = users.Select(x => GenerateUserMentionLine(x.Key, x.Value));
            var line = string.Join(", ", mentions);
            _lines.Add(line);
            return this;
        }

        public WebexTeamsMessageBuilder AddLink(string link, string display = null)
        {
            ParameterValidator.IsPopulated(link, "Link");

            if (string.IsNullOrWhiteSpace(display))
                _lines.Add(link);
            else
                _lines.Add($"[{display}]({link})");

            return this;
        }

        public string Build()
        {
            ParameterValidator.AreValuesPopulated(_lines, "Lines");

            var markdown = string.Join(WebexTeamsFormatting.LineBreak, _lines);
            return JsonConvert.SerializeObject(new { markdown = markdown });
        }

        private WebexTeamsMessageBuilder AddList(string prompt, string[] items, Func<string, string> func)
        {
            ParameterValidator.AreValuesPopulated(items, "Items");

            if (!string.IsNullOrWhiteSpace(prompt))
                AddLine(prompt);

            var listItems = items.Where(x => !string.IsNullOrWhiteSpace(x)).Select(func);

            return AddLine(string.Join(Environment.NewLine, listItems) + Environment.NewLine + WebexTeamsFormatting.LineBreak);
        }

        private string GenerateUserMentionLine(string userName, string nickName)
        {
            ParameterValidator.IsPopulated(userName, "User name");

            var displayName = string.IsNullOrWhiteSpace(nickName) ? userName : nickName;
            return $"<@personEmail:{userName}|{displayName}>";
        }
    }
}