using Godot;
using System;

public class ConsumeBreakfastAction : GoapAction<Eater> 
{
    public ConsumeBreakfastAction() : base("ConsumeBreakfast")
    {
    }
    public override GoapGoal<Eater> GetSuccessorGoal(GoapActionArgs args)
    {
        return new ConsumeBreakfastGoal();
    }
    public override bool Valid(GoapState<Eater> state)
    {
        return state.CheckVarMatch(DoBreakfastGoal.BreakfastIsMade.Name, true);
    }
    public override float Cost(GoapState<Eater> state)
    {
        return 1f;
    }
    public override string Descr(GoapActionArgs args)
    {
        return "Consuming breakfast";
    }
    public override GoapActionArgs ApplyToState(GoapState<Eater> state)
    {
        state.MutateVar(DoBreakfastGoal.HasConsumedBreakfast, true);
        return new GoapActionArgs();
    }
}
