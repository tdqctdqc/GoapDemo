using System;
using System.Collections.Generic;

namespace GoapDemo.BreakfastTest
{
    public class MakeBreakfastAction : GoapAction<Eater>
    {
        [ExplicitVar] private static GoapVar<bool, Eater> _breakfastIsMade 
            = BoolVar<Eater>.ConstructEqualityHeuristic("BreakfastIsMade", 1f, e => e.Bread.Buttered && e.Bread.Toasted && e.Coffee.Made);
        
        [SuccessorVar] private static GoapVar<bool, Eater> _breadIsToasted 
            = BoolVar<Eater>.ConstructEqualityHeuristic("BreadIsToasted", 1f, e => e.Bread.Toasted);
        
        [SuccessorVar] private static GoapVar<bool, Eater> _breadIsButtered 
            = BoolVar<Eater>.ConstructEqualityHeuristic("BreadIsButtered", 1f, e => e.Bread.Buttered);
        
        [SuccessorVar] private static GoapVar<bool, Eater> _coffeeIsMade 
            = BoolVar<Eater>.ConstructEqualityHeuristic("CoffeeIsMade", 1f, e => e.Coffee.Made);
    
    
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
