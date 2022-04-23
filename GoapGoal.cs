using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

public abstract class GoapGoal<TAgent>
{
    public IReadOnlyList<GoapSubGoal<TAgent>> SubGoals => _subGoals;
    private List<GoapSubGoal<TAgent>> _subGoals;
    public List<IGoapAgentVar<TAgent>> ExplicitVars => _explicitVars;
    private List<IGoapAgentVar<TAgent>> _explicitVars;
    public List<IGoapAgentVar<TAgent>> ImplicitVars => _implicitVars;
    private List<IGoapAgentVar<TAgent>> _implicitVars;
    public abstract float Priority(GoapAgent<TAgent> agent);
    public GoapGoal()
    {
        SetupVars(this);
        SetupSubGoals(this);
        CheckActionVars();
    }

    private void CheckActionVars()
    {
        foreach (var subGoal in SubGoals)
        {
            foreach (var action in subGoal.Actions)
            {
                foreach (var actionVar in action.ExplicitVars)
                {
                    if (
                        ExplicitVars
                            .Where(v => v.Name == actionVar.Name
                                        && v.ValueType == actionVar.ValueType)
                            .Count() == 0
                    )
                    {
                        throw new Exception("can't fulfil action var " + actionVar.Name);
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
            ExplicitVars.Select(v => v.BranchAgnosticByAgentEntity(agent.Entity)).ToArray()
        );
        return initialState;
    }

    protected static void SetupVars(GoapGoal<TAgent> goal)
    {
        var goalType = goal.GetType();
        var fields = goalType.GetAllFields();
        goal._explicitVars = fields.GetValuesForFieldsWithAttribute<ExplicitVarAttribute, IGoapAgentVar<TAgent>>();
        goal._implicitVars = fields.GetValuesForFieldsWithAttribute<ImplicitVarAttribute, IGoapAgentVar<TAgent>>();
    }

    protected static void SetupSubGoals(GoapGoal<TAgent> goal)
    {
        var goalType = goal.GetType();
        var fields = goalType.GetAllFields();
        goal._subGoals = fields.GetValuesForFieldsWithAttribute<SubGoalAttribute, GoapSubGoal<TAgent>>();
    }
}
