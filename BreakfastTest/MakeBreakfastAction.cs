using System;
using System.Collections.Generic;

namespace GoapDemo.BreakfastTest
{
    public class MakeBreakfastAction : GoapAction<Eater>
    {
        [ExplicitVar] private static GoapVar<bool, Eater> _breakfastIsMade => DoBreakfastGoal.BreakfastIsMade; 
        [SuccessorVar] private static GoapVar<bool, Eater> _breadIsToasted => MakeBreakfastGoal.BreadIsToasted;
        [SuccessorVar] private static GoapVar<bool, Eater> _breadIsButtered => MakeBreakfastGoal.BreadIsButtered;
        [SuccessorVar] private static GoapVar<bool, Eater> _coffeeIsMade => MakeBreakfastGoal.CoffeeIsMade;
    
        public MakeBreakfastAction() : base("MakeBreakfast", a => { })
        {
        }

        public override GoapState<Eater> TransformContextForSuccessorGoal(GoapState<Eater> actionContext)
        {
            if (actionContext.GetVar<bool>(_breakfastIsMade.Name) is GoapFluent<bool, Eater> breakfastMade)
            {
                GoapState<Eater> initState = new GoapState<Eater>
                (
                    new GoapFluent<bool,Eater>(_breadIsButtered, breakfastMade.Value),
                    new GoapFluent<bool,Eater>(_breadIsToasted, breakfastMade.Value),
                    new GoapFluent<bool,Eater>(_coffeeIsMade, breakfastMade.Value)
                );

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
            state.MutateFluent(_breakfastIsMade, true);
            return new GoapActionArgs();
        }
    }
}
