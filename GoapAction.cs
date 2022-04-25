using Godot;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

public abstract class GoapAction<TAgent> : IGoapAction
{
    public string Name { get; private set; }
    public IReadOnlyList<Func<GoapState<TAgent>, bool>> PreReqs => _preReqs;
    private List<Func<GoapState<TAgent>, bool>> _preReqs;
    public IReadOnlyList<IGoapAgentVar<TAgent>> ExplicitVars => _explicitVars;
    private List<IGoapAgentVar<TAgent>> _explicitVars;
    protected GoapAction(string name, Action<GoapAction<TAgent>> setDependentInstanceFields)
    {
        Name = name;
        setDependentInstanceFields(this);
        SetupReqs(this);
        SetupVars(this);
    }
    public abstract GoapGoal<TAgent> GetSuccessorGoal(GoapActionArgs args);
    public abstract float Cost(GoapState<TAgent> state);
    public abstract string Descr(GoapActionArgs args);
    public abstract GoapState<TAgent> TransformContextForSuccessorGoal(GoapState<TAgent> actionContext);
    public abstract GoapActionArgs ApplyToState(GoapState<TAgent> state);
    private static void SetupVars(GoapAction<TAgent> action)
    {
        action._explicitVars =
            action.GetValuesForMembersWithAttributeType<IGoapAgentVar<TAgent>, ExplicitVarAttribute>();
    }
    private static void SetupReqs(GoapAction<TAgent> action)
    {
        action._preReqs = action.GetValuesForMembersWithAttributeType<Func<GoapState<TAgent>, bool>, RequirementAttribute>();
    }
    public bool Valid(GoapState<TAgent> state)
    {
        for (int i = 0; i < _preReqs.Count; i++)
        {
            if (_preReqs[i](state) == false) return false;
        }

        return true;
    }
    public void CheckRules()
    {
        GoapChecker.CheckActionSuccessorVars(this);
    }
}
