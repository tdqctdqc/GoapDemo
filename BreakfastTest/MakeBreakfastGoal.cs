using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class MakeBreakfastGoal : GoapGoal<Eater>
{
    private static GoapVar<bool, Eater> _breadIsToasted =
        BoolVar<Eater>.Construct("BreadIsToasted", 1f, e => e.Bread.Toasted);
    private static GoapVar<bool, Eater> _breadIsButtered =
        BoolVar<Eater>.Construct("BreadIsButtered", 1f, e => e.Bread.Buttered);
    private static GoapVar<bool, Eater> _coffeeIsMade =
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
            _breadIsButtered,
            _breadIsToasted,
            _coffeeIsMade
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
            Vars.Select(v => v.BranchAgnosticByAgentEntity(eater.Entity)).ToArray()
        );
        return initialState;
    }

    private class MakeToastSubGoal : GoapSubGoal<Eater>
    {
        public override List<GoapAction<Eater>> Actions => _actions;
        private static List<GoapAction<Eater>> _actions;
        protected override void BuildActions()
        {
            _actions = new List<GoapAction<Eater>>()
            {
                new ToastBreadAction(),
                new PutButterOnToastAction()
            };
        }
        public MakeToastSubGoal(float difficulty) : base(GetTargetState(), difficulty)
        {
        }
        private static GoapState<Eater> GetTargetState()
        {
            var targetState = new GoapState<Eater>
            (
                new GoapVarInstance<bool, Eater>(_breadIsToasted, true),
                new GoapVarInstance<bool, Eater>(_breadIsButtered, true)
            );
            return targetState;
        }
    }
    private class MakeCoffeeSubGoal : GoapSubGoal<Eater>
    {
        public override List<GoapAction<Eater>> Actions => _actions;
        private static List<GoapAction<Eater>> _actions;
        protected override void BuildActions()
        {
            _actions = new List<GoapAction<Eater>>()
            {
                new MakeCoffeeAction()
            };
        }
        public MakeCoffeeSubGoal(float difficulty) : base(GetTargetState(), difficulty)
        {
        }
        private static GoapState<Eater> GetTargetState()
        {
            var targetState = new GoapState<Eater>
            (
                new GoapVarInstance<bool, Eater>(_coffeeIsMade, true)
            );
            return targetState;
        }
    }
}













