using Godot;
using System;

public class Root : Node
{
    public override void _Ready()
    {
        DivisionTest();
    }

    private void WalkTest()
    {
        var walker = new Walker();
        var agent = new WalkerAgent(walker);
        var goal = new WalkHomeGoal();
        var plan = GoapPlanner.PlanOld(agent, goal, 100);
        plan?.Print();
    }

    private void ArmyTest()
    {
        var army = new Army(Vector2.Zero, Vector2.Up);
        var agent = new ArmyAgent(army);
        var goal = new DestroyEnemyGoal();
        var plan = GoapPlanner.PlanOld(agent, goal, 100);
        plan?.Print();
    }

    private void DivisionTest()
    {
        var div1 = new Division(Vector2.One * 20f, Vector2.Zero);
        var div2 = new Division(Vector2.One * 20f, Vector2.Zero);
        var agent1 = new DivisionAgent(div1);
        var agent2 = new DivisionAgent(div2);
        
        
        var enemy = new Army(Vector2.Zero, Vector2.Up);
        var coverGoal = new CoverGoal(enemy);
        var flankGoal = new FlankGoal(enemy);
        var coverPlan = GoapPlanner.PlanOld(agent1, coverGoal, 100);
        coverPlan.Print();
        var flankPlan = GoapPlanner.PlanOld(agent2, flankGoal, 100);
        flankPlan.Print();
    }
}
