using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class DoBreakfastGoal : GoapGoal<Eater>
{
    public static GoapVar<bool, Eater> BreakfastIsMade =
        BoolVar<Eater>.Construct("BreakfastIsMade", 1f, e => e.Bread.Buttered && e.Bread.Toasted && e.Coffee.Made);
    public static GoapVar<bool, Eater> HasConsumedBreakfast =
        BoolVar<Eater>.Construct("HasEatenBreakfast", 1f, e => e.Hungry == false);
    public DoBreakfastGoal() : base()
    {
        
    }
    public override float Priority(GoapAgent<Eater> agent)
    {
        return 1f;
    }

    protected override void SetupVars()
    {
        Vars = new List<IGoapAgentVar<Eater>>()
        {
            BreakfastIsMade,
            HasConsumedBreakfast
        };
    }

    protected override void SetupSubGoals()
    {
        SubGoals = new List<GoapSubGoal<Eater>>()
        {
            new DoBreakfastSubGoal(1f)
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

    private class DoBreakfastSubGoal : GoapSubGoal<Eater>
    {
        public override List<GoapAction<Eater>> Actions => _actions;
        private static List<GoapAction<Eater>> _actions = new List<GoapAction<Eater>>()
        {
            new MakeBreakfastAction(),
            new ConsumeBreakfastAction()
        };
        public DoBreakfastSubGoal(float diff) : base(GetTargetState(), diff)
        {
        
        }
        private static GoapState<Eater> GetTargetState()
        {
            var targetState = new GoapState<Eater>();
            targetState.MutateVar(HasConsumedBreakfast, true);
            return targetState;
        }
    }
}


