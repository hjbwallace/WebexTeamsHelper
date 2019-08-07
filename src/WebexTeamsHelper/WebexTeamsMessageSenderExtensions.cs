using System.Threading.Tasks;

namespace WebexTeamsHelper
{
    public static class WebexTeamsMessageSenderExtensions
    {
        public static bool Send(this IWebexTeamsMessageSender webexTeamsMessageSender, string webhookUri, string payload)
        {
            return webexTeamsMessageSender.SendAsync(webhookUri, payload).GetAwaiter().GetResult();
        }

        public static bool Send(this IWebexTeamsMessageSender webexTeamsMessageSender, string webhookUri, WebexTeamsMessage message)
        {
            return webexTeamsMessageSender.Send(webhookUri, message.ToString());
        }

        public static async Task<bool> SendAsync(this IWebexTeamsMessageSender webexTeamsMessageSender, string webhookUri, WebexTeamsMessage message)
        {
            return await webexTeamsMessageSender.SendAsync(webhookUri, message.ToString());
        }
    }
}