# WebexTeamsHelper
Tools to build and send messages via a Webex Teams webhook

## Message Builder

Create a message that can be sent to Webex Teams via a webhook using the `WebexTeamsMessageBuilder`. Builder returns a `WebexTeamsMessage` object which represents the json payload that is sent as a message to Webex Teams via a webhook.

```c#
new WebexTeamsMessageBuilder()
	.AddHeading("Greetings!")
	.AddLine("Some message to the user or room")
	.AddLines("Some message", "On another line")
	.AddLink("http://example.com", "Google")
	.AddLink("www.yahoo.com")
	.AddCodeBlock("SELECT * FROM [Table]")
	.AddQuote("Some fantastic quote")
```

## Message Sender

Use the built in message sender to send a message via a webhook. Returns a boolean indicating whether the request returned a successful status code. 

Either a WebexTeamsMessage or a json payload can be sent from the built-in sender.

The synchronous method `Send` is also available as an extension method on the `IWebexTeamsMessageSender` interface.

### Send a WebexTeamsMessage

```c#
var message = new WebexTeamsMessageBuilder()
	.AddHeading("Greetings!")
	.AddMention("joebloggs@example.com", "Joe")
	.AddLine("Some message to the user or room")
	.Build();
	
var sender = new WebexTeamsMessageSender();

var success = await sender.SendAsync(webhookUri, message);
```

### Send a JSON payload

```c#
var payload = @"{""markdown"":""**Heading**  \r\n  \r\nThis should be *emphasised* as it is important""}";
var sender = new WebexTeamsMessageSender();

var success = await sender.SendAsync(webhookUri, payload);
```