using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebexTeamsHelper
{
    public class WebexTeamsMessageSender : IWebexTeamsMessageSender
    {
        public async Task<bool> SendAsync(string webhookUri, string payload)
        {
            ParameterValidator.IsPopulated(webhookUri, "Webhook uri");
            ParameterValidator.IsPopulated(payload, "Payload");

            using (var client = new HttpClient())
            {
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(webhookUri, content);

                return response.IsSuccessStatusCode;
            }
        }
    }
}