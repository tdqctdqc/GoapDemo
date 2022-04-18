using Godot;
using System;

public class PutButterOnToastAction : GoapAction<Eater>
{
    public PutButterOnToastAction() : base("PutButterOnToast")
    {
    }

    public override GoapGoal<Eater> GetSuccessorGoal(GoapActionArgs args)
    {
        return null;
    }

    public override bool Valid(GoapState<Eater> state)
    {
        return state.CheckVarMatch<bool>(MakeToastSubGoal.BreadIsToasted, true);
    }

    public override float Cost(GoapState<Eater> state)
    {
        return 1f;
    }

    public override string Descr(GoapActionArgs args)
    {
        return "Putting butter on toast";
    }

    public override GoapActionArgs ApplyToState(GoapState<Eater> state)
    {
        state.MutateVar(MakeToastSubGoal.BreadIsButtered, true);
        return new GoapActionArgs();
    }
}
