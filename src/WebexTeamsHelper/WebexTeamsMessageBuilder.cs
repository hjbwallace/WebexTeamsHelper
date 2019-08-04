using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebexTeamsHelper
{
    public class WebexTeamsMessageBuilder
    {
        private const string _lineBreak = "  \n";
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
            return AddLine("> " + quote);
        }

        public WebexTeamsMessageBuilder AddHeading(string heading, bool additionalLineBreak = true)
        {
            _lines.Add(WebexTeamsFormatting.Bold(heading));

            if (additionalLineBreak)
                _lines.Add(_lineBreak);

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

            _lines.Add("```");
            _lines.AddRange(codeLines);
            _lines.Add("```");
            return this;
        }

        public WebexTeamsMessageBuilder AddMention(string userEmail, string nickName = null)
        {
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
            var mentions = users.Select(x => GenerateUserMentionLine(x.Key, x.Value));
            var line = string.Join(", ", mentions);
            _lines.Add(line);
            return this;
        }

        public WebexTeamsMessageBuilder AddLink(string link, string display = null)
        {
            if (string.IsNullOrWhiteSpace(display))
                _lines.Add(link);
            else
                _lines.Add($"[{display}]({link})");

            return this;
        }

        public string Build()
        {
            var markdown = string.Join(_lineBreak, _lines);
            return JsonConvert.SerializeObject(new { markdown = markdown });
        }

        private WebexTeamsMessageBuilder AddList(string prompt, string[] items, Func<string, string> func)
        {
            if (!string.IsNullOrWhiteSpace(prompt))
                AddLine(prompt);

            var listItems = items.Where(x => !string.IsNullOrWhiteSpace(x)).Select(func);

            return AddLine(string.Join(Environment.NewLine, listItems) + Environment.NewLine + _lineBreak);
        }

        private string GenerateUserMentionLine(string userName, string nickName)
        {
            var displayName = string.IsNullOrWhiteSpace(nickName) ? userName : nickName;
            return $"<@personEmail:{userName}|{displayName}>";
        }
    }
}