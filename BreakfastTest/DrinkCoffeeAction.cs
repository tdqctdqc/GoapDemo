using System.Collections.Generic;

namespace GoapDemo.BreakfastTest
{
    public class DrinkCoffeeAction : GoapAction<Eater>
    {
        public static GoapVar<bool, Eater> Caffeinated =
            BoolVar<Eater>.Construct("Caffeinated", 1f, e => e.Caffeinated);
    
        public DrinkCoffeeAction() : base("DrinkCoffee")
        {
        }

        protected override void SetupVars()
        {
            Vars = new List<IGoapAgentVar<Eater>>
            {
                Caffeinated
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
            return "Drinking coffee";
        }
        public override GoapActionArgs ApplyToState(GoapState<Eater> state)
        {
            state.MutateVar(Caffeinated, true);
            return new GoapActionArgs();
        }
    }
}
