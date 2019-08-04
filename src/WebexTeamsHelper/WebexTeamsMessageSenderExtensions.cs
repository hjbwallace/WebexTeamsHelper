using System.Collections.Generic;

namespace WebexTeamsHelper
{

    public static class WebexTeamsMessageSenderExtensions
    {
        public static bool Send(this IWebexTeamsMessageSender webexTeamsMessageSender, string webhookUri, string payload)
        {
            return webexTeamsMessageSender.SendAsync(webhookUri, payload).GetAwaiter().GetResult();
        }
    }
}
