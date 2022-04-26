using Godot;
using System;
using System.Reflection;
using System.Collections.Generic;
using GoapDemo.BreakfastTest;
using GoapDemo.WalkHomeTest;

public class Root : Node
{
    public override void _Ready()
    {
        // GoapChecker.CheckRules();
        
        // DoWalkerTest();
        // DoBreakfastTest(1);
        DoErrandsTest();
        // DoReflectionTest();
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
        var goal = new RunErrandsGoal(Vector2.Right * 10f, Vector2.Up * 10f, Vector2.One * 10f);
        GoapPlanner.PlanGoal(goal, list);
    }
}
