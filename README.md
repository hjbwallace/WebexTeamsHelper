# WebexTeamsHelper
Tools to assist when interacting with Webex Teams

## Message Builder

Create the json payload that is sent as the message to Webex Teams in a fluent manner via a webhook.

```c#
new WebexTeamsMessageBuilder()
	.AddHeading("Greetings!")
	.AddLine("Some message to the user or room")
	.AddLink("http://example.com", "Google")
	.AddLink("www.yahoo.com")
	.AddCodeBlock("SELECT * FROM [Table]")
	.AddQuote("Some fantastic quote")
```

## Message Sender


Use the built in message sender to send a message as a given webhook. Returns a boolean indicating whether the request returned a successful status code.

```c#
var webhookUri = "www.someuri.com"

var builder = new WebexTeamsMessageBuilder()
	.AddHeading("Greetings!")
	.AddLine("Some message to the user or room");
	
var sender = new WebexTeamsMessageSender();

var success = await sender.SendAsync(webhookUri, builder.Build());
```

The synchronous method `Send` is also available as an extension method on the `IWebexTeamsMessageSender` interface.


