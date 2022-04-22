using System;
using System.Collections.Generic;

namespace GoapDemo.BreakfastTest
{
    public class MakeBreakfastAction : GoapAction<Eater>
    {
        [ExplicitVar] private static GoapVar<bool, Eater> _breakfastIsMade =
            BoolVar<Eater>.Construct("BreakfastIsMade", 1f, e => e.Bread.Buttered && e.Bread.Toasted && e.Coffee.Made);
    
        [SuccessorVar] private static GoapVar<bool, Eater> _breadIsToasted =
            BoolVar<Eater>.Construct("BreadIsToasted", 1f, e => e.Bread.Toasted);
        [SuccessorVar] private static GoapVar<bool, Eater> _breadIsButtered =
            BoolVar<Eater>.Construct("BreadIsButtered", 1f, e => e.Bread.Buttered);
        [SuccessorVar] private static GoapVar<bool, Eater> _coffeeIsMade =
            BoolVar<Eater>.Construct("CoffeeIsMade", 1f, e => e.Coffee.Made);
    
    
        public MakeBreakfastAction() : base("MakeBreakfast")
        {
        }

        public override GoapState<Eater> TransformContextForSuccessorGoal(GoapState<Eater> actionContext)
        {
            if (actionContext.GetVar<bool>(_breakfastIsMade.Name) is GoapFluent<bool, Eater> breakfastMade)
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
            state.MutateVar(_breakfastIsMade, true);
            return new GoapActionArgs();
        }
    }
}
