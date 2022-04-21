using System;
using System.Collections.Generic;

namespace GoapDemo.BreakfastTest
{
    public class MakeBreakfastAction : GoapAction<Eater>
    {
        public static GoapVar<bool, Eater> BreakfastIsMade =
            BoolVar<Eater>.Construct("BreakfastIsMade", 1f, e => e.Bread.Buttered && e.Bread.Toasted && e.Coffee.Made);
    
        private static GoapVar<bool, Eater> _breadIsToasted =
            BoolVar<Eater>.Construct("BreadIsToasted", 1f, e => e.Bread.Toasted);
        private static GoapVar<bool, Eater> _breadIsButtered =
            BoolVar<Eater>.Construct("BreadIsButtered", 1f, e => e.Bread.Buttered);
        private static GoapVar<bool, Eater> _coffeeIsMade =
            BoolVar<Eater>.Construct("CoffeeIsMade", 1f, e => e.Coffee.Made);
    
    
        public MakeBreakfastAction() : base("MakeBreakfast")
        {
        }

        protected override void SetupVars()
        {
            ExplicitVars = new List<IGoapAgentVar<Eater>>
            {
                BreakfastIsMade
            };
            SuccessorVars = new List<IGoapAgentVar<Eater>>
            {
                _breadIsButtered,
                _breadIsToasted,
                _coffeeIsMade
            };
        }

        public override GoapState<Eater> TransformContextForSuccessorGoal(GoapState<Eater> actionContext)
        {
            if (actionContext.GetVar<bool>(BreakfastIsMade.Name) is GoapFluent<bool, Eater> breakfastMade)
            {
                GoapState<Eater> initState;
                if (breakfastMade.Value == false)
                {
                    initState = new GoapState<Eater>
                    (
                        new GoapFluent<bool,Eater>(_breadIsButtered, false),
                        new GoapFluent<bool,Eater>(_breadIsToasted, false),
                        new GoapFluent<bool,Eater>(_coffeeIsMade, false)
                    );
                }
                else
                {
                    initState = new GoapState<Eater>
                    (
                        new GoapFluent<bool,Eater>(_breadIsButtered, true),
                        new GoapFluent<bool,Eater>(_breadIsToasted, true),
                        new GoapFluent<bool,Eater>(_coffeeIsMade, true)
                    );
                }

                return initState;
            }
            throw new Exception();
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
            state.MutateVar(BreakfastIsMade, true);
            return new GoapActionArgs();
        }
    }
}
