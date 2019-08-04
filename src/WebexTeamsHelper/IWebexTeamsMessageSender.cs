using System.Threading.Tasks;

namespace WebexTeamsHelper
{
    public interface IWebexTeamsMessageSender
    {
        Task<bool> SendAsync(string webhookUri, string payload);
    }
}