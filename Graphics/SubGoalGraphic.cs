using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class SubGoalGraphic : Node2D
{
    private Label _label;
    private object _subGoal;
    private object _search;
    private List<PlanGraphic> _planGraphics;
    private PackedScene _planGraphicScene = (PackedScene) GD.Load("res://Graphics/PlanGraphic.tscn");
    private PlanGraphic _planGraphic => (PlanGraphic) _planGraphicScene.Instance();
    public override void _Ready()
    {
        _label = (Label)FindNode("Label");
        _planGraphics = new List<PlanGraphic>();
    }

    public void Setup<TAgent>(GoapSubGoal<TAgent> subGoal)
    {
        _subGoal = subGoal;
        _label.Text = subGoal.Name + " Sub Goal";
    }

    public void StartSearch<TAgent>(GoapState<TAgent> startState)
    {
        GD.Print("starting search");

        if (_subGoal is GoapSubGoal<TAgent> s == false) throw new Exception();
        _search = new GoapPlanAStarSearch<TAgent>(s, startState, 100);
    }

    public void CycleSearch<TAgent>()
    {
        GD.Print("cycling search");
        if (_search is GoapPlanAStarSearch<TAgent> search == false) throw new Exception();
        if (_subGoal is GoapSubGoal<TAgent> subGoal == false) throw new Exception();
        _planGraphics.ForEach(g => g.Free());
        _planGraphics.Clear();
        var openPlans = search.CycleAndGetOpenNodes();
        GD.Print("open plans " + openPlans.Count());
        for (int i = 0; i < openPlans.Count(); i++)
        {
            var planGraphic = _planGraphic;
            _planGraphics.Add(planGraphic);
            AddChild(planGraphic);
            planGraphic.Setup(openPlans.ElementAt(i), subGoal.TargetState);
            planGraphic.Position = Position + Vector2.Down * 100f + Vector2.Left * i * 100f;
        }
    }
}
