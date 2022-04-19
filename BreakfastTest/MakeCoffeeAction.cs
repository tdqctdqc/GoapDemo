using Godot;
using System;

public class MakeCoffeeAction : GoapAction<Eater>
{
    public MakeCoffeeAction() : base("MakeCoffee")
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
        return "Making coffee";
    }
    public override GoapActionArgs ApplyToState(GoapState<Eater> state)
    {
        var coffeeMadeVar = MakeBreakfastGoal.CoffeeIsMade;
        state.MutateVar(coffeeMadeVar, true);
        return new GoapActionArgs();
    }
}
