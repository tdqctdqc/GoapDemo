using Godot;
using System;
using System.Collections.Generic;

public class MakeBreakfastGoal : GoapGoal<Eater>
{
    public MakeBreakfastGoal() : base()
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
            new MakeToastSubGoal(1f),
            new MakeCoffeeSubGoal(1f)
        };
    }
}










public class MakeToastSubGoal : GoapSubGoal<Eater>
{
    public static GoapVar<bool, Eater> BreadIsToasted =
        BoolVar<Eater>.Construct("BreadIsToasted", 1f, e => e.Bread.Toasted);
    public static GoapVar<bool, Eater> BreadIsButtered =
        BoolVar<Eater>.Construct("BreadIsButtered", 1f, e => e.Bread.Buttered);

    public MakeToastSubGoal(float difficulty) : base(GetTargetState(), difficulty)
    {
    }
    private static GoapState<Eater> GetTargetState()
    {
        var targetState = new GoapState<Eater>();
        targetState.MutateVar(BreadIsToasted, true);
        targetState.MutateVar(BreadIsButtered, true);
        return targetState;
    }

    public override GoapState<Eater> GetInitialState(List<GoapAgent<Eater>> agents)
    {
        var eater = agents[0];
        var initialState = new GoapState<Eater>();
        initialState.MutateVar(BreadIsToasted, eater.Entity.Bread.Toasted);
        initialState.MutateVar(BreadIsButtered, eater.Entity.Bread.Buttered);
        return initialState;
    }

    protected override void SetupActions()
    {
        Actions = new List<GoapAction<Eater>>()
        {
            new ToastBreadAction(),
            new PutButterOnToastAction()
        };
    }
}

public class MakeCoffeeSubGoal : GoapSubGoal<Eater>
{
    public static GoapVar<bool, Eater> CoffeeIsMade =
        BoolVar<Eater>.Construct("CoffeeIsMade", 1f, e => e.Coffee.Made);
    public MakeCoffeeSubGoal(float difficulty) : base(GetTargetState(), difficulty)
    {
    }
    private static GoapState<Eater> GetTargetState()
    {
        var targetState = new GoapState<Eater>();
        targetState.MutateVar(CoffeeIsMade, true);
        return targetState;
    }

    public override GoapState<Eater> GetInitialState(List<GoapAgent<Eater>> agents)
    {
        var eater = agents[0]; 
        var initialState = new GoapState<Eater>();
        initialState.MutateVar(CoffeeIsMade, eater.Entity.Coffee.Made);
        return initialState;
    }

    protected override void SetupActions()
    {
        Actions = new List<GoapAction<Eater>>()
        {
            new MakeCoffeeAction()
        };
    }
}
