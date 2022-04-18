using Godot;
using System;
using System.Collections.Generic;

public class GoapActionNode<TAgent>
{
    public GoapAction<TAgent> Action { get; private set; }
    public GoapPlanNode<TAgent> Parent { get; private set; }
    public GoapActionArgs Args { get; private set; }

    public GoapActionNode(GoapAction<TAgent> action, GoapPlanNode<TAgent> parent,
        GoapActionArgs args)
    {
        Args = args; 
        Action = action;
        Parent = parent;
    }
}
