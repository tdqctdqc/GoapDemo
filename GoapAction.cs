using Godot;
using System;
using System.Collections.Generic;

public abstract class GoapAction<TAgent> : IGoapAction
{
    public string Name { get; private set; }
    public List<IGoapVar> Reqs { get; private set; }
    protected GoapAction(string name)
    {
        Name = name;
        Reqs = new List<IGoapVar>();
    }

    public abstract GoapGoal<TAgent> GetSuccessorGoal(GoapActionArgs args);
    public abstract bool Valid(GoapState<TAgent> state);
    public abstract float Cost(GoapState<TAgent> state);
    public abstract string Descr(GoapActionArgs args);
    public abstract GoapActionArgs ApplyToState(GoapState<TAgent> state);
}
