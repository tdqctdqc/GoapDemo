using Godot;
using System;
using System.Collections.Generic;
using GoapDemo.BreakfastTest;
using GoapDemo.WalkHomeTest;

public class Root : Node
{
    public override void _Ready()
    {
        DoWalkerTest();
    }

    private void DoBreakfastTest()
    {
        var eater = new Eater();
        var agent = new EaterAgent(eater);
        var list = new List<GoapAgent<Eater>> {agent};
        var goal = new DoBreakfastGoal();
        GoapPlanner.PlanGoal(goal, list);
    }

    private void DoWalkerTest()
    {
        var walker = new Walker(Vector2.One * 100f, Vector2.Zero, true, 10f);
        var agent = new WalkerAgent(walker);
        var list = new List<GoapAgent<Walker>> {agent};
        var goal = new WalkHomeGoal();
        GoapPlanner.PlanGoal(goal, list);
    }
}
