using System.Collections.Generic;

namespace GoapDemo.BreakfastTest
{
    public class DrinkCoffeeAction : GoapAction<Eater>
    {
        [ExplicitVar] private static GoapVar<bool, Eater> _caffeinated =
            BoolVar<Eater>.ConstructEqualityHeuristic("Caffeinated", 1f, e => e.Caffeinated);
    
        public DrinkCoffeeAction() : base("DrinkCoffee", a => { })
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
            return "Drinking coffee";
        }
        public override GoapActionArgs ApplyToState(GoapState<Eater> state)
        {
            state.MutateFluent(_caffeinated, true);
            return new GoapActionArgs();
        }
    }
}
