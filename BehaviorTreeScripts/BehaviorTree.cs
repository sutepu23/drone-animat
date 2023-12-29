using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BehaviorTree : Node
{
    public BehaviorTree()
    {
        name = "Tree";
    }

    public BehaviorTree(string name)
    {
        this.name = name;
    }

    public override Status Process()
    {
        return childrenNodes[currChild].Process();
    }

    struct NodeLevel 
    {
        public int level;
        public Node node;
    }

    public void PrintTree()
    {
        string treePrint = "";
        Stack<NodeLevel> nodeStack = new Stack<NodeLevel>();
        Node currentNode = this;
        nodeStack.Push(new NodeLevel { level = 0, node = currentNode });

        while (nodeStack.Count != 0)
        {
            NodeLevel nextNode = nodeStack.Pop();
            treePrint += new string('-', nextNode.level) + nextNode.node.name + "\n";
            for (int i = nextNode.node.childrenNodes.Count - 1; i >= 0; i--)
            {
                nodeStack.Push(new NodeLevel { level = nextNode.level + 1, node = nextNode.node.childrenNodes[i] });
            }
        }

        Debug.Log(treePrint);
    }
}
