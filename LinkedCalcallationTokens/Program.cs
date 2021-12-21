CancellationTokenSource externalCTS = new CancellationTokenSource(5000);
CancellationToken externalToken = externalCTS.Token;

CancellationTokenSource linkedCTS = CancellationTokenSource.CreateLinkedTokenSource(externalToken);
CancellationToken linkedToken = linkedCTS.Token;

try
{
    using var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));

    int counter = 0;

    while (await timer.WaitForNextTickAsync(linkedToken))
    {
        counter++;

        Console.WriteLine("Iteration #" + counter.ToString());

        if (counter > 12)
        {
            linkedCTS.Cancel();
        }
    }
}
catch (OperationCanceledException)
{
    if (externalCTS.IsCancellationRequested)
        Console.WriteLine("Canceled by the external token");
    else
        Console.WriteLine("Canceled by the internal token");
}
