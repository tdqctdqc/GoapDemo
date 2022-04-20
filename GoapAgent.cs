using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract class GoapAgent<TAgent> : IGoapAgent
{
    public TAgent Entity { get; private set; }
    public GoapAgent(TAgent entity)
    {
        Entity = entity; 
    }
}
