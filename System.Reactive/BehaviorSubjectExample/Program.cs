using ExtensionsLibrary;
using System;
using System.Reactive.Subjects;

namespace BehaviorSubjectExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BehaviorSubject<NetworkConnectivity> connection = new BehaviorSubject<NetworkConnectivity>(NetworkConnectivity.Disconnected);

            connection.SubscribeConsole("first");

            // After connection
            connection.OnNext(NetworkConnectivity.Connected);

            connection.SubscribeConsole("second");

            Console.WriteLine("Connection is '{0}'", connection.Value);

            Console.ReadLine();
        }
    }

    public enum NetworkConnectivity
    {
        Disconnected,
        Connected
    }
}
