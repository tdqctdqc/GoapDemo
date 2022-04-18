using Godot;
using System;

public class AttackAction : GoapAction<Division>
{
    private Army _enemy; 
    public AttackAction(Army enemy) : base("Attack")
    {
        _enemy = enemy;
    }

    public override GoapGoal<Division> GetSuccessorGoal(GoapActionArgs args)
    {
        return null;
    }

    public override bool Valid(GoapState<Division> state)
    {
        return state.GetVar<Vector2>(DivisionAgent.PositionVar).Value.DistanceTo(_enemy.Position) <= 10f; 
        return true; 
    }

    public override float Cost(GoapState<Division> state)
    {
        return 1f; 
    }

    public override string Descr(GoapActionArgs args)
    {
        return "Attacking";
    }

    public override GoapActionArgs ApplyToState(GoapState<Division> state)
    {
        state.MutateVar(DivisionAgent.AttackingVar, true);
        var args = new GoapActionArgs();
        return args; 
    }
}
