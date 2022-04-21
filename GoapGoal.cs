using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract class GoapGoal<TAgent> 
{
    public List<GoapSubGoal<TAgent>> SubGoals { get; protected set; }
    public List<IGoapAgentVar<TAgent>> Vars { get; protected set; }
    public abstract float Priority(GoapAgent<TAgent> agent);
    public GoapGoal()
    {
        SetupVars();
        SetupSubGoals();
        CheckActionVars();
    }

    protected abstract void SetupVars();
    protected abstract void SetupSubGoals();

    private void CheckActionVars()
    {
        foreach (var subGoal in SubGoals)
        {
            foreach (var action in subGoal.Actions)
            {
                foreach (var actionVar in action.Vars)
                {
                    if (
                        Vars
                            .Where(v => v.Name == actionVar.Name
                                        && v.ValueType == actionVar.ValueType)
                            .Count() == 0
                    )
                    {
                        throw new Exception("can't fulfil action vars");
                    }
                }
            }
        }
    }
    public abstract GoapState<TAgent> GetInitialState(List<GoapAgent<TAgent>> agents);

    protected GoapState<TAgent> GetInitialStateFirstAgentMethod(List<GoapAgent<TAgent>> agents)
    {
        var agent = agents[0];
        var initialState = new GoapState<TAgent>
        (
            Vars.Select(v => v.BranchAgnosticByAgentEntity(agent.Entity)).ToArray()
        );
        return initialState;
    }
}
