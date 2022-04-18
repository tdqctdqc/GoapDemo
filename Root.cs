using Godot;
using System;
using System.Collections.Generic;

public class Root : Node
{
    public override void _Ready()
    {
        var eater = new Eater();
        var agent = new EaterAgent(eater);
        var list = new List<GoapAgent<Eater>> {agent};
        var goal = new DoBreakfastGoal();
        GoapPlanner.PlanGoal(goal, list);
    }
}
