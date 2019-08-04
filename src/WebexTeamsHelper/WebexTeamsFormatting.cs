namespace WebexTeamsHelper
{
    public static class WebexTeamsFormatting
    {
        public static string Inline(string source) => $"`{source}`";

        public static string Bold(string source) => $"**{source}**";

        public static string Italic(string source) => $"*{source}*";
    }
}