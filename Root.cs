using Godot;
using System;
using System.Reflection;
using System.Collections.Generic;
using GoapDemo.BreakfastTest;
using GoapDemo.WalkHomeTest;

public class Root : Node
{
    private CameraController _camera;
    private PackedScene _goalGraphicScene = (PackedScene) GD.Load("res://Graphics/GoalGraphic.tscn");
    private GoalGraphic _getGoalGraphic => (GoalGraphic) _goalGraphicScene.Instance();
    private GoalGraphic _goalGraphic;
    private Action _cycle;

    public override void _Ready()
    {
        _camera = GetNode<CameraController>("Camera");
        _camera.Current = true;
        // GoapChecker.CheckRules();
        var list = new List<GoapAgent<Errander>> {new ErrandAgent(new Errander(Vector2.Zero))};
        var goal = new RunErrandsGoal(Vector2.Right * 10f, Vector2.Up * 10f, Vector2.One * 10f);
        _goalGraphic = DoGraphicTest(goal, list);
        DoErrandsTest();
        
        // DoReflectionTest();
    }

    public override void _Input(InputEvent evnt)
    {
        _camera.Input(evnt);
        if (evnt is InputEventKey k)
        {
            if (k.Scancode == (int) KeyList.Space && k.Pressed == false)
            {
                _cycle();
            }
        }
    }

    private void DoBreakfastTest(int agentCount)
    {
        var list = new List<GoapAgent<Eater>>();
        for (int i = 0; i < agentCount; i++)
        {
            var eater = new Eater();

            list.Add(new EaterAgent(eater));
        }
        var goal = new DoBreakfastGoal();
        GoapPlanner.PlanGoal(goal, list);
    }

    private void DoWalkerTest()
    {
        var list = new List<GoapAgent<Walker>>();

        for (int i = 0; i < 3; i++)
        {
            var walker = new Walker(Vector2.One * 100f, Vector2.Zero, true, 10f);

            list.Add(new WalkerAgent(walker));
        }
        var goal = new WalkHomeGoal();
        GoapPlanner.PlanGoal(goal, list);
    }

    private void DoErrandsTest()
    {
        var list = new List<GoapAgent<Errander>>{new ErrandAgent(new Errander(Vector2.Zero))};
        var goal = new RunErrandsGoal(Vector2.Right, Vector2.Up, Vector2.One);
        GoapPlanner.PlanGoal(goal, list);
    }

    private GoalGraphic DoGraphicTest<TAgent>(GoapGoal<TAgent> goapGoal, List<GoapAgent<TAgent>> agents)
    {
        var goalGraphic = _getGoalGraphic;
        AddChild(goalGraphic);
        goalGraphic.Setup(goapGoal);
        goalGraphic.StartSearch(agents);
        _cycle = goalGraphic.Cycle<TAgent>;
        return goalGraphic;
    }
}
