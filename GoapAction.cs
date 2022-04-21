using Godot;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

public abstract class GoapAction<TAgent> : IGoapAction
{
    public string Name { get; private set; }
    public List<IGoapVar> Reqs { get; private set; }
    public abstract GoapState<TAgent> TransformContextForSuccessorGoal(GoapState<TAgent> actionContext);
    public List<IGoapAgentVar<TAgent>> ExplicitVars => _explicitVars;
    private List<IGoapAgentVar<TAgent>> _explicitVars;

    public List<IGoapAgentVar<TAgent>> SuccessorVars => _successorVars;
    private List<IGoapAgentVar<TAgent>> _successorVars;
    protected GoapAction(string name)
    {
        Name = name;
        Reqs = new List<IGoapVar>();
        SetupVarsSpecial(this);
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
        foreach (var goalVar in goal.ExplicitVars)
        {
            if (
                SuccessorVars
                    .Where(v => v.Name == goalVar.Name
                                && v.ValueType == goalVar.ValueType)
                    .Count() == 0
            )
            {
                throw new Exception("can't fulfil successor goal var " + goalVar.Name);
            }
        }
    }
    protected static void SetupVarsSpecial(GoapAction<TAgent> goal)
    {
        var goalType = goal.GetType();
        var fields = goalType.GetFields(BindingFlags.NonPublic | BindingFlags.Static);
        goal._explicitVars = fields.GetFieldsWithAttribute<ExplicitVarAttribute, IGoapAgentVar<TAgent>>();
        goal._successorVars = fields.GetFieldsWithAttribute<SuccessorVarAttribute, IGoapAgentVar<TAgent>>();
    }
}
