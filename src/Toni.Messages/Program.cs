
using Toni.Messages.Domain;
using Toni.Messages.Infrastructure;
using Toni.Messages.Infrastructure.Implementations;

var serializer = new Serializer();
var receiver = new MessageReceiver(serializer);
var messages = new Messages(new MessageSender(), serializer);

var json =
$@"
[
    {{
        <message-json-placeholder>
    }}
]
";

receiver.Receive(json);

var occasion = Console.ReadLine();
receiver.Submit(occasion);