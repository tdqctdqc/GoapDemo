using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

public abstract class GoapGoal<TAgent> : IGoapGoal
{
    public IReadOnlyList<GoapSubGoal<TAgent>> SubGoals => _subGoals;
    private List<GoapSubGoal<TAgent>> _subGoals;
    public List<IGoapAgentVar<TAgent>> ExplicitVars => _explicitVars;
    private List<IGoapAgentVar<TAgent>> _explicitVars;
    public List<IGoapAgentVar<TAgent>> ImplicitVars => _implicitVars;
    private List<IGoapAgentVar<TAgent>> _implicitVars;
    public abstract float Priority(GoapAgent<TAgent> agent);
    public GoapGoal(Action<GoapGoal<TAgent>> setDependentInstanceFields)
    {
        setDependentInstanceFields(this);
        SetupVars(this);
        SetupSubGoals(this);
        // CheckActionVars();
        // CheckImplicitVars();
    }

    
    public abstract GoapState<TAgent> GetInitialState(List<GoapAgent<TAgent>> agents);

    protected GoapState<TAgent> GetInitialStateFirstAgentMethod(List<GoapAgent<TAgent>> agents)
    {
        var agent = agents[0];
        var initialState = new GoapState<TAgent>
        (
            ExplicitVars.Select(v => v.BranchAgnosticByAgentEntity(agent.Entity)).ToArray()
        );
        return initialState;
    }

    protected static void SetupVars(GoapGoal<TAgent> goal)
    {
        goal._explicitVars = goal.GetValuesForMembersWithAttributeType<IGoapAgentVar<TAgent>, ExplicitVarAttribute>();
        goal._implicitVars = goal.GetValuesForMembersWithAttributeType<IGoapAgentVar<TAgent>, ImplicitVarAttribute>();
    }

    protected static void SetupSubGoals(GoapGoal<TAgent> goal)
    {
        goal._subGoals = goal.GetValuesForMembersWithAttributeType<GoapSubGoal<TAgent>, SubGoalAttribute>();
    }

    public void CheckRules()
    {
        GoapChecker.CheckActionVars(this);
        GoapChecker.CheckImplicitVars(this);
    }
}
