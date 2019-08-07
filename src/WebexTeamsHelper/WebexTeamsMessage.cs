using Newtonsoft.Json;

namespace WebexTeamsHelper
{
    public class WebexTeamsMessage
    {
        public WebexTeamsMessage(string markdown)
        {
            Markdown = markdown;
        }

        [JsonProperty("markdown")]
        public string Markdown { get; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
