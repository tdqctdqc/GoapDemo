using Godot;
using System;
using System.Collections.Generic;

public class GoapMultiAgent<TAgent> : GoapAgent<List<TAgent>>
{
    public GoapMultiAgent(List<TAgent> entity) : base(entity)
    {
        
    }
}
