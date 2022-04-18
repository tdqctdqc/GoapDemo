using Godot;
using System;

public class MakeBreakfastAction : GoapAction<Eater>
{
    public MakeBreakfastAction() : base("MakeBreakfast")
    {
    }

    public override GoapGoal<Eater> GetSuccessorGoal(GoapActionArgs args)
    {
        return new MakeBreakfastGoal();
    }

    public override bool Valid(GoapState<Eater> state)
    {
        return true;
    }

    public override float Cost(GoapState<Eater> state)
    {
        return 1f;
    }

    public override string Descr(GoapActionArgs args)
    {
        return "Making breakfast";
    }

    public override GoapActionArgs ApplyToState(GoapState<Eater> state)
    {
        state.MutateVar(DoBreakfastSubGoal.BreakfastIsMade, true);
        return new GoapActionArgs();
    }
}
