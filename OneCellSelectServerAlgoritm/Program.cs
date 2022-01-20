var items = new List<string> { "one", "two", "three", "four", "five" };
int itemsCount = items.Count;
int pointer = 1;

Parallel.For(0, 10, x =>
{
    var localPointer = Interlocked.Increment(ref pointer);

    var itemIndex = localPointer % itemsCount;

    var item = items[itemIndex];

    Console.WriteLine($"x = {x}, localPointer = {localPointer}, itemIndex = {itemIndex}, item = {item}");
});

Queue<string> itemsQueue = new Queue<string>(items);
object locker = new object();

string GetNextItem()
{
    lock (locker)
    {
        var item = itemsQueue.Dequeue();

        itemsQueue.Enqueue(item);

        return item;
    }
}

Parallel.For(0, 10, x =>
{
    var item = GetNextItem();

    Console.WriteLine($"x = {x}, item = {item}");
});
