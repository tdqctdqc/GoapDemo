using Godot;
using System;
using System.Collections.Generic;

public abstract class GoapGoal<TAgent> 
{
    public List<GoapSubGoal<TAgent>> SubGoals { get; protected set; }
    public abstract float Priority(GoapAgent<TAgent> agent);
    public GoapGoal()
    {
        SetupSubGoals();
    }

    protected abstract void SetupSubGoals();
}
