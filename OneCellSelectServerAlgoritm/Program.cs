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