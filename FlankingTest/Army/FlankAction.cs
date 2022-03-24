using Godot;
using System;

public class FlankAction : GoapAction<Army>
{
    public FlankAction() : base("Flank")
    {
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
}
