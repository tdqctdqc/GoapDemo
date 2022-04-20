using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class ConsumeBreakfastGoal : GoapGoal<Eater>
{
    public static GoapVar<bool, Eater> Hungry =
        BoolVar<Eater>.Construct("Hungry", 1f, e => e.Hungry);
    public static GoapVar<bool, Eater> Caffeinated =
        BoolVar<Eater>.Construct("Caffeinated", 1f, e => e.Caffeinated);
    public ConsumeBreakfastGoal() : base()
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
            Hungry,
            Caffeinated
        };
    }

    protected override void SetupSubGoals()
    {
        SubGoals = new List<GoapSubGoal<Eater>>()
        {
            new ConsumeBreakfastSubGoal(1f)
        };
    }

    public override GoapState<Eater> GetInitialState(List<GoapAgent<Eater>> agents)
    {
        var eater = agents[0];
        var initialState = new GoapState<Eater>
        (
            Vars.Select(v => v.BranchAgnosticByAgent(eater)).ToArray()
        );
        return initialState;
    }

    private class ConsumeBreakfastSubGoal : GoapSubGoal<Eater>
    {
        public override List<GoapAction<Eater>> Actions => _actions;
        private static List<GoapAction<Eater>> _actions = new List<GoapAction<Eater>>()
        {
            new EatToastAction(),
            new DrinkCoffeeAction()
        };
        public ConsumeBreakfastSubGoal(float difficulty) : base(GetTargetState(), difficulty)
        {
        }
        private static GoapState<Eater> GetTargetState()
        {
            var targetState = new GoapState<Eater>
            (
                new GoapVarInstance<bool, Eater>(Hungry, false),
                new GoapVarInstance<bool, Eater>(Caffeinated, true)
            );
            return targetState;
        }
    }
    private class EatToastAction : GoapAction<Eater>
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
            state.MutateVar(ConsumeBreakfastGoal.Hungry, false);
            return new GoapActionArgs();
        }
    }
    private class DrinkCoffeeAction : GoapAction<Eater>
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
            state.MutateVar(ConsumeBreakfastGoal.Caffeinated, true);
            return new GoapActionArgs();
        }
    }
    public class MakeBreakfastAction : GoapAction<Eater>
    {
        public MakeBreakfastAction() : base("MakeBreakfast")
        {
        }
        public override GoapGoal<Eater> GetSuccessorGoal(GoapActionArgs args)
        {
            return new MakeBreakfastGoal();
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
            return "Making breakfast";
        }
        public override GoapActionArgs ApplyToState(GoapState<Eater> state)
        {
            state.MutateVar(DoBreakfastGoal.BreakfastIsMade, true);
            return new GoapActionArgs();
        }
    }
}


