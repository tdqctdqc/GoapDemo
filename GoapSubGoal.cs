using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class GoapSubGoal<TAgent>
{
    public GoapState<TAgent> TargetState { get; private set; }
    public IReadOnlyList<GoapAction<TAgent>> Actions => _actions;
    private List<GoapAction<TAgent>> _actions; 
    public IReadOnlyList<GoapAgentRequirement<TAgent>> AgentRequirements => _agentRequirements;
    private List<GoapAgentRequirement<TAgent>> _agentRequirements;
    protected GoapSubGoal(Action<GoapSubGoal<TAgent>> setDependentInstanceFields)
    {
        setDependentInstanceFields(this);
        // TargetState = targetState;
        BuildTargetState(this);
        BuildActions(this);
        BuildAgentReqs(this);
    }
    private GoapSubGoal(params GoapSubGoal<TAgent>[] subGoals)
    {
        TargetState = GoapState<TAgent>.GetUnionState(
            subGoals.Select(s => s.TargetState).ToArray()
        );
        _actions = subGoals.SelectMany(s => s.Actions).ToHashSet().ToList();
        _agentRequirements = subGoals.SelectMany(s => s.AgentRequirements).ToList();
    }
    public static GoapSubGoal<TAgent> GetUnionSubGoal(params GoapSubGoal<TAgent>[] subGoals)
    {
        return new GoapSubGoal<TAgent>(subGoals);
    }
    private static void BuildAgentReqs(GoapSubGoal<TAgent> subGoal)
    {
        subGoal._agentRequirements = subGoal
            .GetValuesForMembersWithAttributeType<GoapAgentRequirement<TAgent>, AgentRequirementAttribute>();
        if (subGoal._agentRequirements.Count == 0)
            throw new Exception(subGoal.GetType() + " needs at least one agent req");
    }
    private static void BuildActions(GoapSubGoal<TAgent> subGoal)
    {
        subGoal._actions = subGoal
            .GetValuesForMembersWithAttributeType<GoapAction<TAgent>, AvailableActionAttribute>();
    }

    private static void BuildTargetState(GoapSubGoal<TAgent> subGoal)
    {
        var targetFluents = subGoal
            .GetValuesForMembersWithAttributeType<IGoapAgentFluent<TAgent>, TargetFluentAttribute>();
        subGoal.TargetState = new GoapState<TAgent>(targetFluents.ToArray());
    }
}
