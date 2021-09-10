using ExtensionsLibrary;

namespace ObservableBaseExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var dummyChatClient = new ChatClient();
            var subscription = dummyChatClient.Connect("guest", "pwd123")
                .ToObservable()
                .SubscribeConsole();
        }
    }
}
