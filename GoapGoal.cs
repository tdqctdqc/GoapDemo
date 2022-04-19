using Godot;
using System;
using System.Collections.Generic;

public abstract class GoapGoal<TAgent> 
{
    public List<GoapSubGoal<TAgent>> SubGoals { get; protected set; }
    public List<IGoapAgentVar<TAgent>> Vars { get; protected set; }
    public abstract float Priority(GoapAgent<TAgent> agent);
    public GoapGoal()
    {
        SetupVars();
        SetupSubGoals();
    }

    protected abstract void SetupVars();
    protected abstract void SetupSubGoals();
    public abstract GoapState<TAgent> GetInitialState(List<GoapAgent<TAgent>> agents);
}
