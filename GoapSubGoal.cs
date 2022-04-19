using Godot;
using System;
using System.Collections.Generic;

public abstract class GoapSubGoal<TAgent>
{
    public GoapState<TAgent> TargetState { get; private set; }
    public abstract List<GoapAction<TAgent>> Actions { get; }
    public float Difficulty { get; private set; }
    public abstract GoapState<TAgent> GetInitialState(List<GoapAgent<TAgent>> agents);
    protected GoapSubGoal(GoapState<TAgent> targetState, float difficulty)
    {
        Difficulty = difficulty;
        TargetState = targetState;
    }
    public float SubordinateCapability(IGoapAgent agent)
    {
        return 0f;
    }
}
