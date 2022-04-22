using System;
using System.Collections.Generic;

namespace GoapDemo.BreakfastTest
{
    public class MakeCoffeeAction : GoapAction<Eater>
    {
        [ExplicitVar] private static GoapVar<bool, Eater> _coffeeIsMade =
            BoolVar<Eater>.Construct("CoffeeIsMade", 1f, e => e.Coffee.Made);

        public MakeCoffeeAction() : base("MakeCoffee")
        {
        }

        public override GoapState<Eater> TransformContextForSuccessorGoal(GoapState<Eater> actionContext)
        {
            return null;
        }

        public override GoapGoal<Eater> GetSuccessorGoal(GoapActionArgs args)
        {
            return null;
        }
        public override float Cost(GoapState<Eater> state)
        {
            return 1f; 
        }
        public override string Descr(GoapActionArgs args)
        {
            return "Making coffee";
        }
        public override GoapActionArgs ApplyToState(GoapState<Eater> state)
        {
            var coffeeMadeVar = _coffeeIsMade;
            state.MutateVar(coffeeMadeVar, true);
            return new GoapActionArgs();
        }
    }
}
