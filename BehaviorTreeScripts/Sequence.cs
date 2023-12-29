using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
    public Sequence(string name)
    {
        this.name = name;
    }

    public override Status Process()
    {
        Status childStatus = childrenNodes[currChild].Process();
        if (childStatus == Status.Running)
        {
            return Status.Running;
        }
        if (childStatus == Status.Failed)
        {
            return childStatus;
        }
        currChild++;
        if(currChild >= childrenNodes.Count)
        {
            currChild = 0;
            return Status.Success;
        }
        return Status.Running;
    }
}
