using RecursiveLinqTest;

var node11 = new Node(11);
node11.Children.AddRange(Enumerable.Range(12, 9).Select(x => new Node(x)));

var nodeLevel3 = new Node(3);
nodeLevel3.Children.AddRange(Enumerable.Range(4, 7).Select(x => new Node(x)));
nodeLevel3.Children.Add(node11);

var nodeLevel2 = new Node(2);
nodeLevel2.Children.Add(nodeLevel3);

var rootNode = new Node(1);
rootNode.Children.Add(nodeLevel2);

ExecuteSearch(rootNode, 3);
ExecuteSearch(rootNode, 5);
ExecuteSearch(rootNode, 10);
ExecuteSearch(rootNode, 16);

void ExecuteSearch(Node node, int nodeId)
{
    Console.WriteLine("Searching the node with id " + nodeId);
    
    var target = node.GetAllInner().FirstOrDefault(x => x.Id == nodeId);

    Console.WriteLine(target is null ? "Target node was not found" : "Target node was found");
    Console.WriteLine();
}