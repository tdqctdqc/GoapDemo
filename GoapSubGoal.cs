using Godot;
using System;
using System.Collections.Generic;

public abstract class GoapSubGoal<TAgent>
{
    public GoapState<TAgent> TargetState { get; private set; }
    public List<GoapAction<TAgent>> Actions { get; protected set; }
    public float Difficulty { get; private set; }

    public abstract GoapState<TAgent> GetInitialState(List<GoapAgent<TAgent>> agents);
    protected abstract void SetupActions();
    protected GoapSubGoal(GoapState<TAgent> targetState, float difficulty)
    {
        Difficulty = difficulty;
        TargetState = targetState;
        SetupActions();
    }
    public float SubordinateCapability(IGoapAgent agent)
    {
        return 0f;
    }
}
