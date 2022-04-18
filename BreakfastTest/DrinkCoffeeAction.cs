using Godot;
using System;

public class DrinkCoffeeAction : GoapAction<Eater>
{
    public DrinkCoffeeAction() : base("DrinkCoffee")
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
        return "Drinking coffee";
    }

    public override GoapActionArgs ApplyToState(GoapState<Eater> state)
    {
        state.MutateVar(ConsumeBreakfastSubGoal.Caffeinated, true);
        return new GoapActionArgs();
    }
}
