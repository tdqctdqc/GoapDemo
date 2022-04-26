using System;
using System.Collections.Generic;
using System.Linq;

namespace GoapDemo.BreakfastTest
{
public class DoBreakfastGoal : GoapGoal<Eater>
{
    [ExplicitVar] public static GoapVar<bool, Eater> BreakfastIsMade 
        = BoolVar<Eater>.ConstructEqualityHeuristic("BreakfastIsMade", 
            1f, e => e.Bread.Buttered && e.Bread.Toasted && e.Coffee.Made);

    [ExplicitVar] public static GoapVar<bool, Eater> HasConsumedBreakfast 
        = BoolVar<Eater>.ConstructEqualityHeuristic("HasEatenBreakfast", 1f, e => e.Hungry == false);

    [SubGoal] private static GoapSubGoal<Eater> _subGoal 
        =  new DoBreakfastSubGoal(1f);
    [TestCase] private static IGoapGoal GetTestCase() => new DoBreakfastGoal();
    public DoBreakfastGoal() : base("DoBreakfast", g => { })
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

