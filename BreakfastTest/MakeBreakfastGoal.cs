using System.Collections.Generic;
using System.Linq;

namespace GoapDemo.BreakfastTest
{
    public class MakeBreakfastGoal : GoapGoal<Eater>
    {
        [ExplicitVar] public static GoapVar<bool, Eater> BreadIsToasted 
            = BoolVar<Eater>.ConstructEqualityHeuristic("BreadIsToasted", 1f, e => e.Bread.Toasted);
        [ExplicitVar] public static GoapVar<bool, Eater> BreadIsButtered 
            = BoolVar<Eater>.ConstructEqualityHeuristic("BreadIsButtered", 1f, e => e.Bread.Buttered);
        [ExplicitVar] public static GoapVar<bool, Eater> CoffeeIsMade 
            = BoolVar<Eater>.ConstructEqualityHeuristic("CoffeeIsMade", 1f, e => e.Coffee.Made);

        [SubGoal] private static GoapSubGoal<Eater> _makeToastSubGoal
            = new MakeToastSubGoal();
        [SubGoal] private static GoapSubGoal<Eater> _makeCoffeeSubGoal
            = new MakeCoffeeSubGoal();
        [TestCase] private static IGoapGoal GetTestCase() => new MakeBreakfastGoal();
        public MakeBreakfastGoal() : base("MakeBreakfast", g => { })
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













