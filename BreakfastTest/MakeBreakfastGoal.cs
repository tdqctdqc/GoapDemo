using System.Collections.Generic;
using System.Linq;

namespace GoapDemo.BreakfastTest
{
    public class MakeBreakfastGoal : GoapGoal<Eater>
    {
        [ExplicitVar] private static GoapVar<bool, Eater> _breadIsToasted 
            = BoolVar<Eater>.ConstructEqualityHeuristic("BreadIsToasted", 1f, e => e.Bread.Toasted);
        [ExplicitVar] private static GoapVar<bool, Eater> _breadIsButtered 
            = BoolVar<Eater>.ConstructEqualityHeuristic("BreadIsButtered", 1f, e => e.Bread.Buttered);
        [ExplicitVar] private static GoapVar<bool, Eater> _coffeeIsMade 
            = BoolVar<Eater>.ConstructEqualityHeuristic("CoffeeIsMade", 1f, e => e.Coffee.Made);

        [SubGoal] private static GoapSubGoal<Eater> _makeToastSubGoal
            = new MakeToastSubGoal();
        [SubGoal] private static GoapSubGoal<Eater> _makeCoffeeSubGoal
            = new MakeCoffeeSubGoal();
        public MakeBreakfastGoal() : base(() => { })
        {
        
        }
        public override float Priority(GoapAgent<Eater> agent)
        {
            return 1f;
        }

        public override GoapState<Eater> GetInitialState(List<GoapAgent<Eater>> agents)
        {
            return GetInitialStateFirstAgentMethod(agents);
        }
    }
}













