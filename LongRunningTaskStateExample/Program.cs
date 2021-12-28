CancellationTokenSource cts = new CancellationTokenSource(3000);

var longRunningTask = Task.Factory.StartNew(async () =>
{
    await Task.Delay(4000, cts.Token);
}, cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);

var underlyingTask = longRunningTask.Unwrap();

Console.WriteLine($"Long Running Task status after launch is: {longRunningTask.Status}");
Console.WriteLine($"Underlying Task status after launch is: {underlyingTask.Status}");

await Task.Delay(1000);

Console.WriteLine($"Long Running Task status after 1 sec. is: {longRunningTask.Status}");
Console.WriteLine($"Underlying Task status after 1 sec. is: {underlyingTask.Status}");

await Task.Delay(4000);

Console.WriteLine($"Long Running Task status after 4 sec. is: {longRunningTask.Status}");
Console.WriteLine($"Underlying Task status after 4 sec. is: {underlyingTask.Status}");

try
{
    await underlyingTask;
}
catch (OperationCanceledException)
{
    Console.WriteLine("Inderlying Task was successfully completed");
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}
finally
{
    Console.WriteLine($"Long running Task status is: {underlyingTask.Status}");
    Console.WriteLine($"Long running Task.IsCompleted: {underlyingTask.IsCompleted}");
}
