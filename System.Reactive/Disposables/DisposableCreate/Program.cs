using System.Reactive.Disposables;

PrintBusyState(true);

using (Disposable.Create(() => PrintBusyState(false)))
{
    Console.WriteLine("Waiting for a long task");

    await Task.Delay(2000);

    Console.WriteLine("The long task finished");
}

Console.ReadLine();

void PrintBusyState(bool busy)
{
    Console.WriteLine($"Is Busy = {busy}");
}
