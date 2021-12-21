TimeSpan period = TimeSpan.FromSeconds(1);

CancellationTokenSource cts = new(TimeSpan.FromMilliseconds(3500));

DateTime startTime = DateTime.Now;

try
{
    using PeriodicTimer timer = new(period);

    while (await timer.WaitForNextTickAsync(cts.Token))
    {
        var timeDifference = (DateTime.Now - startTime).Milliseconds;

        Console.WriteLine($"Offset: {timeDifference}");
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}
