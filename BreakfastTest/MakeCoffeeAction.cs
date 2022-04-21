using System.Collections.Generic;

namespace GoapDemo.BreakfastTest
{
    public class MakeCoffeeAction : GoapAction<Eater>
    {
        public static GoapVar<bool, Eater> CoffeeIsMade =
            BoolVar<Eater>.Construct("CoffeeIsMade", 1f, e => e.Coffee.Made);
        public MakeCoffeeAction() : base("MakeCoffee")
        {
        }

        protected override void SetupVars()
        {
            Vars = new List<IGoapAgentVar<Eater>>
            {
                CoffeeIsMade
            };
        }

        public override GoapState<Eater> TransformContextForSuccessorGoal(GoapState<Eater> actionContext)
        {
            return null;
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
            return "Making coffee";
        }
        public override GoapActionArgs ApplyToState(GoapState<Eater> state)
        {
            var coffeeMadeVar = CoffeeIsMade;
            state.MutateVar(coffeeMadeVar, true);
            return new GoapActionArgs();
        }
    }
}
