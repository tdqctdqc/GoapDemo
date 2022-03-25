using Godot;
using System;

public class FlankAction : GoapAction<Army>
{
    private Army _enemy; 
    public FlankAction(Army enemy) : base("Flank")
    {
        _enemy = enemy; 
    }

    public override bool Valid(GoapState<Army> state)
    {
        return state.GetVar<bool>(ArmyAgent.EnemyIsFlanked).Value == false;
    }

    public override float Cost(GoapState<Army> state)
    {
        return 1f; 
    }

    public override string Descr(GoapActionArgs args)
    {
        return "Flanking Enemy Army";
    }

    public override GoapActionArgs ApplyToState(GoapState<Army> state)
    {
        state.MutateVar(ArmyAgent.EnemyIsFlanked, true);
        return new GoapActionArgs();
    }
    public override GoapGoal<TSubAgent> GetAssocGoal<TSubAgent>(GoapActionArgs args)
    {
        if (typeof(TSubAgent).IsAssignableFrom( typeof(Division) ) )
        {
            return new FlankGoal(_enemy) as GoapGoal<TSubAgent>;
        }
        return null;
    }
}
