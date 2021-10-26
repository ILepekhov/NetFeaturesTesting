using System.Reactive.Linq;

var messenger = new Messenger();
var messages = Observable
    .FromEventPattern<string>(
        h => messenger.MessageReceived += h,
        h => messenger.MessageReceived -= h);

messages
    .Select(evt => evt.EventArgs)
    .Synchronize()
    .Subscribe(msg =>
    {
        Console.WriteLine($"Message {msg} arrived");
        Thread.Sleep(1000);
        Console.WriteLine($"Message {msg} processed");
    });

messenger.RaiseMessageReceived();

Console.ReadLine();


