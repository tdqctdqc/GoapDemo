using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract class GoapAction<TAgent> : IGoapAction
{
    public string Name { get; private set; }
    public List<IGoapVar> Reqs { get; private set; }
    protected abstract void SetupVars();
    public abstract GoapState<TAgent> TransformContextForSuccessorGoal(GoapState<TAgent> actionContext);
    public List<IGoapAgentVar<TAgent>> Vars { get; protected set; }
    public List<IGoapAgentVar<TAgent>> SuccessorVars { get; protected set; }
    protected GoapAction(string name)
    {
        Name = name;
        Reqs = new List<IGoapVar>();
        SetupVars();
        CheckSuccessorGoalActions();
    }

    public abstract GoapGoal<TAgent> GetSuccessorGoal(GoapActionArgs args);
    public abstract bool Valid(GoapState<TAgent> state);
    public abstract float Cost(GoapState<TAgent> state);
    public abstract string Descr(GoapActionArgs args);
    public abstract GoapActionArgs ApplyToState(GoapState<TAgent> state);

    private void CheckSuccessorGoalActions()
    {
        var goal = GetSuccessorGoal(new GoapActionArgs());
        if (goal == null) return;
        foreach (var goalVar in goal.Vars)
        {
            if (
                SuccessorVars
                    .Where(v => v.Name == goalVar.Name
                                && v.ValueType == goalVar.ValueType)
                    .Count() == 0
            )
            {
                throw new Exception("can't fulfil action vars");
            }
        }
    }
}
