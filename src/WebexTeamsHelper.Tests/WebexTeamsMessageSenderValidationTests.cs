using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WebexTeamsHelper.Tests
{
    public class WebexTeamsMessageSenderValidationTests : ValidationTestBase
    {
        private readonly WebexTeamsMessageSender _sender = new WebexTeamsMessageSender();

        [Theory]
        [StringValidation]
        public void ThrowsWhenWebhookUriIsNullOrEmpty(string webhookUri)
        {
            ThrowsArgumentException(() => _sender.Send(webhookUri, "{}"));
        }

        [Theory]
        [StringValidation]
        public void ThrowsWhenPayloadIsNullOrEmpty(string payload)
        {
            ThrowsArgumentException(() => _sender.Send("uri", payload));
        }
    }
}
