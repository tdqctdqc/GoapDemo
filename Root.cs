using Godot;
using System;

public class Root : Node
{
    public override void _Ready()
    {
        ArmyTest();
    }

    private void WalkTest()
    {
        var walker = new Walker();
        var agent = new WalkerAgent(walker);
        var goal = new WalkHomeGoal();
        var plan = GoapPlanner.Plan(agent, goal, 100);
        plan?.Print();
    }

    private void ArmyTest()
    {
        var walker = new Army();
        var agent = new ArmyAgent(walker);
        var goal = new DestroyEnemyGoal();
        var plan = GoapPlanner.Plan(agent, goal, 100);
        plan?.Print();
    }
}
