using ExtensionsLibrary;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

Task<string[]> facebookMessages = Task.Delay(TimeSpan.FromSeconds(1)).ContinueWith(_ => new[] { "Facebook-1", "Facebook-2" });
Task<string[]> twitterMessages = Task.FromResult(new[] { "Twitter-1", "Twitter-2" });

Observable.Concat(facebookMessages.ToObservable(), twitterMessages.ToObservable())
    .SelectMany(messages => messages)
    .SubscribeConsole("Concat Messages");

Console.ReadLine();