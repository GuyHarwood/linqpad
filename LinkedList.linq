<Query Kind="Program">
  <Namespace>System</Namespace>
  <Namespace>System.Threading</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	var list = new LinkedList();
	for (int i = 0; i < 100; i++)
	{
		var n = list.Add();
		n.Dump();
	}
}

public class LinkedList
{
	private readonly HashSet<Node> _nodes = new HashSet<Node>();
	
	public Node[] Nodes 
	{
		get
		{
			return _nodes.ToArray();
		}
	}
	
	public Node Add()
	{
		var node = new Node();
		node.Index = _nodes.Count;
		node.PreviousNode = _nodes.LastOrDefault();
		_nodes.Add(node);
		return node;
	}
}

public class Node
{
	public int Index {get;set;}
	public Node PreviousNode {get;set;}
}