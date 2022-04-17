using Godot;
using System;
using System.Collections.Generic;

public class GoapMultiAgent<TAgent> 
{
    public List<GoapAgent<TAgent>> Entities { get; private set; }
    public GoapMultiAgent(List<GoapAgent<TAgent>> entities) 
    {
        
    }
}
