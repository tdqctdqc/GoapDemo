using Godot;
using System;
using System.Collections.Generic;

public abstract class GoapSubGoal<TAgent>
{
    public GoapState<TAgent> TargetState { get; private set; }
    public IReadOnlyList<GoapAction<TAgent>> Actions => _actions;
    private List<GoapAction<TAgent>> _actions; 
    // protected abstract void BuildActions();
    public float Difficulty { get; private set; }
    protected GoapSubGoal(GoapState<TAgent> targetState, float difficulty)
    {
        Difficulty = difficulty;
        TargetState = targetState;
        BuildActionsAttributes(this);
    }

    public abstract float GetAgentCapability(GoapAgent<TAgent> agent);

    private static void BuildActionsAttributes(GoapSubGoal<TAgent> subGoal)
    {
        var subGoalType = subGoal.GetType();
        var fields = subGoalType.GetAllFields();
        subGoal._actions = fields.GetValuesForFieldsWithAttribute<AvailableActionAttribute, GoapAction<TAgent>>();
    }
}
