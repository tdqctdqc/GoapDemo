using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class MakeBreakfastGoal : GoapGoal<Eater>
{
    public static GoapVar<bool, Eater> BreadIsToasted =
        BoolVar<Eater>.Construct("BreadIsToasted", 1f, e => e.Bread.Toasted);
    public static GoapVar<bool, Eater> BreadIsButtered =
        BoolVar<Eater>.Construct("BreadIsButtered", 1f, e => e.Bread.Buttered);
    public static GoapVar<bool, Eater> CoffeeIsMade =
        BoolVar<Eater>.Construct("CoffeeIsMade", 1f, e => e.Coffee.Made);
    public MakeBreakfastGoal() : base()
    {
        
    }
    public override float Priority(GoapAgent<Eater> agent)
    {
        return 1f;
    }

    protected override void SetupVars()
    {
        Vars = new List<IGoapAgentVar<Eater>>
        {
            BreadIsButtered,
            BreadIsToasted,
            CoffeeIsMade
        };
    }

    protected override void SetupSubGoals()
    {
        SubGoals = new List<GoapSubGoal<Eater>>()
        {
            new MakeToastSubGoal(1f),
            new MakeCoffeeSubGoal(1f)
        };
    }

    public override GoapState<Eater> GetInitialState(List<GoapAgent<Eater>> agents)
    {
        var eater = agents[0];
        var initialState = new GoapState<Eater>
        (
            Vars.Select(v => v.BranchAgnostic(eater)).ToArray()
        );
        return initialState;
    }

    private class MakeToastSubGoal : GoapSubGoal<Eater>
    {
        public override List<GoapAction<Eater>> Actions => _actions;
        private static List<GoapAction<Eater>> _actions = new List<GoapAction<Eater>>()
        {
            new ToastBreadAction(),
            new PutButterOnToastAction()
        };
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
    }
    private class MakeCoffeeSubGoal : GoapSubGoal<Eater>
    {
        public override List<GoapAction<Eater>> Actions => _actions;
        private static List<GoapAction<Eater>> _actions = new List<GoapAction<Eater>>()
        {
            new MakeCoffeeAction()
        };
        public MakeCoffeeSubGoal(float difficulty) : base(GetTargetState(), difficulty)
        {
        }
        private static GoapState<Eater> GetTargetState()
        {
            var targetState = new GoapState<Eater>();
            targetState.MutateVar(CoffeeIsMade, true);
            return targetState;
        }
    }
}













