using Newtonsoft.Json;
using System;
using Xunit;

namespace WebexTeamsHelper.Tests
{
    public abstract class PayloadTestBase
    {
        protected string LineBreak => WebexTeamsFormatting.LineBreak;

        protected void AssertPayload(Func<WebexTeamsMessageBuilder, WebexTeamsMessageBuilder> messageBuilderFunc, string markdown)
        {
            var payload = messageBuilderFunc(new WebexTeamsMessageBuilder()).Build();
            var expected = JsonConvert.SerializeObject(new { markdown = markdown });

            Assert.Equal(expected, payload);
        }
    }
}