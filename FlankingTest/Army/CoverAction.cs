using Godot;
using System;

public class CoverAction : GoapAction<Army>
{
    public CoverAction() : base("Cover")
    {
        
    }

    public override bool Valid(GoapState<Army> state)
    {
        return state.GetVar<bool>(ArmyAgent.EnemyIsEngaged).Value == false;
    }

    public override float Cost(GoapState<Army> state)
    {
        return 1f; 
    }

    public override string Descr(GoapActionArgs args)
    {
        return "Covering Enemy Army";
    }

    public override GoapActionArgs ApplyToState(GoapState<Army> state)
    {
        state.MutateVar(ArmyAgent.EnemyIsEngaged, true);
        return new GoapActionArgs();
    }
}
