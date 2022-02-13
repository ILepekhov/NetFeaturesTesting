int ExecuteBinarySearch(int[] array, int item)
{
    var low = 0;
    var high = array.Length - 1;

    while (low <= high)
    {
        var mid = (low + high) / 2;

        var guess = array[mid];

        if (guess == item)
            return mid;

        if (guess > item)
            high = mid - 1;
        else
            low = mid + 1;
    }

    return -1;
}

int[] array = Enumerable.Range(1, 100).ToArray();

Console.WriteLine(ExecuteBinarySearch(array, 3));
Console.WriteLine(ExecuteBinarySearch(array, -10));
