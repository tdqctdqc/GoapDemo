using Godot;
using System;
using System.Collections.Generic;

public abstract class GoapAction<TAgent> : IGoapAction
{
    public string Name { get; private set; }
    public List<IGoapVar> Reqs { get; private set; }
    protected Tuple<Type, object> _assocGoal;
    protected GoapAction(string name)
    {
        Name = name;
        Reqs = new List<IGoapVar>();
    }
    public GoapGoal<TSubAgent> GetAssocGoal<TSubAgent>()
    {
        if (typeof(TSubAgent).IsAssignableFrom(_assocGoal.Item1))
        {
            return (GoapGoal<TSubAgent>) _assocGoal.Item2;
        }
        return null;
    }
    public abstract bool Valid(GoapState<TAgent> state);
    public abstract float Cost(GoapState<TAgent> state);
    public abstract string Descr(GoapActionArgs args);
    public abstract GoapActionArgs ApplyToState(GoapState<TAgent> state);
}
