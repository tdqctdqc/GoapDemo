using Godot;
using System;

public class Root : Node
{
    public override void _Ready()
    {
        var walker = new Walker();
        var agent = new WalkerAgent(walker);
        var goal = new WalkHomeGoal();
        var plan = GoapPlanner.Plan(agent, goal, 100);
        plan?.Print();
    }
}
