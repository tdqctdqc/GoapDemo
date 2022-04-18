using Godot;
using System;

public class ToastBreadAction : GoapAction<Eater>
{
    public ToastBreadAction() : base("PutBreadInToaster")
    {
    }

    public override GoapGoal<Eater> GetSuccessorGoal(GoapActionArgs args)
    {
        return null;
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
        return "Toasting bread";
    }

    public override GoapActionArgs ApplyToState(GoapState<Eater> state)
    {
        var breadToastedVar = MakeToastSubGoal.BreadIsToasted;
        state.MutateVar(breadToastedVar, true);
        return new GoapActionArgs();
    }
}
