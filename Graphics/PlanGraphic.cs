using Godot;
using System;

public class PlanGraphic : Node2D
{
    private object _plan;
    private Label _label;
    private Container _actionsContainer;
    public override void _Ready()
    {
        _label = (Label)FindNode("Label");
        _actionsContainer = (Container) FindNode("ActionsContainer");
    }

    public void Setup<TAgent>(GoapPlan<TAgent> plan, GoapState<TAgent> targetState)
    {
        _plan = plan;
        _label.Text = (plan.Cost + targetState.GetHeuristicDistance(plan.EndState)).ToString();
        if (targetState.SatisfiedBy(plan.EndState))
        {
            GD.Print("success");
            Modulate = Colors.Red;
        }
        for (int i = 0; i < plan.Actions.Count; i++)
        {
            var action = plan.Actions[i];
            var label = new Label();
            label.Text = action.Name;
            _actionsContainer.AddChild(label);
        }
    }
}
