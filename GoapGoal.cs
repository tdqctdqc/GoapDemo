using Godot;
using System;
using System.Collections.Generic;

public abstract class GoapGoal<TAgent> 
{
    public List<GoapAction<TAgent>> Actions { get; protected set; }
    public GoapState<TAgent> TargetState { get; protected set; }
    public abstract GoapState<TAgent> GetInitialState(GoapAgent<TAgent> agent);
    public abstract float Priority(GoapAgent<TAgent> agent);
    public abstract float SubordinateCapability(IGoapAgent agent);
}
