string[] results = new string[2];

Barrier barrier = new Barrier(2, b =>
{
    Console.WriteLine("Results: " + string.Join(", ", results));
});

var task1 = Task.Run(() =>
{
    results[0] = "Thread 1 id: " + Environment.CurrentManagedThreadId;

    barrier.SignalAndWait();
});

Console.WriteLine("Sleeping for 1 sec");

Thread.Sleep(1000);

var task2 = Task.Run(() =>
{
    results[1] = "Thread 2 id: " + Environment.CurrentManagedThreadId;

    barrier.SignalAndWait();
});

Task.WaitAll(task1, task2);

