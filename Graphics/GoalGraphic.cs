using Godot;
using System;
using System.Collections.Generic;

public class GoalGraphic : Node2D
{
    private object _goal;
    private Label _label;
    private List<SubGoalGraphic> _subGoalGraphics;
    private PackedScene _subGoalGraphicScene = (PackedScene)GD.Load("res://Graphics/SubGoalGraphic.tscn");
    private SubGoalGraphic _subGoalGraphic => (SubGoalGraphic)_subGoalGraphicScene.Instance();
    public override void _Ready()
    {
        _label = (Label)FindNode("Label");
        _subGoalGraphics = new List<SubGoalGraphic>();
    }

    public void Setup<TAgent>(GoapGoal<TAgent> goal)
    {
        _goal = goal;
        _label.Text = goal.Name + " Goal";
        for (int i = 0; i < goal.SubGoals.Count; i++)
        {
            var subGoal = goal.SubGoals[i];
            var subGraphic = _subGoalGraphic;
            _subGoalGraphics.Add(subGraphic);
            subGraphic.Position = Position + Vector2.Down * 100f + Vector2.Left * i * 100f;
            AddChild(subGraphic);
            subGraphic.Setup(subGoal);
        }
    }

    public void StartSearch<TAgent>(List<GoapAgent<TAgent>> agents)
    {
        if (_goal is GoapGoal<TAgent> goal == false) throw new Exception();

        for (int i = 0; i < _subGoalGraphics.Count; i++)
        {
            var subGoalGraphic = _subGoalGraphics[i];
            var initState = goal.GetInitialState(agents);
            subGoalGraphic.StartSearch(initState);
        }
    }
    public void Cycle<TAgent>()
    {
        if (_goal is GoapGoal<TAgent> == false) throw new Exception();
        for (int i = 0; i < _subGoalGraphics.Count; i++)
        {
            var subGraphic = _subGoalGraphics[i];
            subGraphic.CycleSearch<TAgent>();
        }
    }
}
