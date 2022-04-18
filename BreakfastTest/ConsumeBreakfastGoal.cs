using Godot;
using System;
using System.Collections.Generic;

public class ConsumeBreakfastGoal : GoapGoal<Eater>
{
    public ConsumeBreakfastGoal() : base()
    {
        
    }
    public override float Priority(GoapAgent<Eater> agent)
    {
        return 1f;
    }

    protected override void SetupSubGoals()
    {
        SubGoals = new List<GoapSubGoal<Eater>>()
        {
            new ConsumeBreakfastSubGoal(1f)
        };
    }
}

public class ConsumeBreakfastSubGoal : GoapSubGoal<Eater>
{
    public static GoapVar<bool, Eater> Hungry =
        BoolVar<Eater>.Construct("Hungry", 1f, e => e.Hungry);
    public static GoapVar<bool, Eater> Caffeinated =
        BoolVar<Eater>.Construct("Caffeinated", 1f, e => e.Caffeinated);
    public ConsumeBreakfastSubGoal(float difficulty) : base(GetTargetState(), difficulty)
    {
    }

    public override GoapState<Eater> GetInitialState(List<GoapAgent<Eater>> agents)
    {
        var eater = agents[0];
        var targetState = new GoapState<Eater>();
        targetState.MutateVar(Hungry, eater.Entity.Hungry);
        targetState.MutateVar(Caffeinated, eater.Entity.Caffeinated);
        return targetState;
    }
    private static GoapState<Eater> GetTargetState()
    {
        var targetState = new GoapState<Eater>();
        targetState.MutateVar(Hungry, false);
        targetState.MutateVar(Caffeinated, true);
        return targetState;
    }

    protected override void SetupActions()
    {
        Actions = new List<GoapAction<Eater>>()
        {
            new EatToastAction(),
            new DrinkCoffeeAction()
        };
    }
}
