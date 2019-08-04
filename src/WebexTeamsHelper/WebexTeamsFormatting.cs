namespace WebexTeamsHelper
{
    public static class WebexTeamsFormatting
    {
        public const string LineBreak = "  \n";

        public static string Inline(string source) => $"`{source}`";

        public static string Bold(string source) => $"**{source}**";

        public static string Italic(string source) => $"*{source}*";
    }
}