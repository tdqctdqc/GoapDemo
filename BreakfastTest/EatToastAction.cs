using Godot;
using System;

public class EatToastAction : GoapAction<Eater>
{
    public EatToastAction() : base("EatToast")
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
        return "Eating toast";
    }

    public override GoapActionArgs ApplyToState(GoapState<Eater> state)
    {
        state.MutateVar(ConsumeBreakfastSubGoal.Hungry, false);
        return new GoapActionArgs();
    }
}
