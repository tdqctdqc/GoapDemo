using Godot;
using System;
using System.Collections.Generic;

public abstract class GoapSubGoal<TAgent>
{
    public GoapState<TAgent> TargetState { get; private set; }
    public abstract List<GoapAction<TAgent>> Actions { get; }
    protected abstract void BuildActions();
    public float Difficulty { get; private set; }
    protected GoapSubGoal(GoapState<TAgent> targetState, float difficulty)
    {
        Difficulty = difficulty;
        TargetState = targetState;
        BuildActions();
    }
    public float SubordinateCapability(IGoapAgent agent)
    {
        return 0f;
    }
}
