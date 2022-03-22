namespace RecursiveLinqTest;

public sealed class Node
{
    public int Id { get; }
    public List<Node> Children { get; }

    public Node(int id)
    {
        Id = id;
        Children = new List<Node>();
    }

    public IEnumerable<Node> GetAllInner()
    {
        Console.WriteLine("requested for node " + Id);
        return Children.Concat(Children.SelectMany(x => x.GetAllInner()));
    }
}