using System.Collections.Generic;
public class Node
{
    public enum Status
    {
        Success,
        Running,
        Failed,
    }

    public Status status;

    public List<Node> childrenNodes = new List<Node>();
    public int currChild;
    public string name;

    public Node()
    {

    }

    public Node(string name)
    {
        this.name = name;
    }

    public virtual Status Process()
    {
        return childrenNodes[currChild].Process();
    }

    public void AddChildren(Node node)
    {
        childrenNodes.Add(node);
    }
}
