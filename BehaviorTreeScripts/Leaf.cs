using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : Node
{
    public delegate Status Tick();
    public Tick ProcessMethod;

    public Leaf()
    {

    }

    public Leaf(string name, Tick pm)
    {
        this.name = name;
        ProcessMethod = pm;
    }
    public override Status Process()
    {
        if(ProcessMethod != null)
        {
            return ProcessMethod();
        }
        else
        {
            return Status.Failed;
        }
    }
}
