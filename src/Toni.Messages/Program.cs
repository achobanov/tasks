
using Toni.Messages.Domain;
using Toni.Messages.Infrastructure;
using Toni.Messages.Infrastructure.Implementations;

var serializer = new Serializer();
var receiver = new MessageReceiver(serializer);
var messages = new Messages(new MessageSender(serializer), serializer);

var staticMessageJson =
$@"
[
    {{
        ""message"": ""THis is a static message""
    }}
]
";
var json2 =
$@"
[
    {{
        ""message"": ""THis is a dynamic message from company {{0}}""
    }}
]
";

var json3 =
$@"
[
    {{
        ""message"": ""THis is a dynamic message from company {{0}} celebration {{1}}""
    }}
]
";

receiver.Receive(staticMessageJson);
receiver.Receive(json2);
receiver.Receive(json2);
receiver.Receive(json3);

var occasion = Console.ReadLine();
receiver.Submit(occasion);