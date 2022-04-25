using System;
using System.Collections.Generic;
using System.Linq;

namespace GoapDemo.BreakfastTest
{
public class DoBreakfastGoal : GoapGoal<Eater>
{
    [ExplicitVar] private static GoapVar<bool, Eater> _breakfastIsMade 
        = BoolVar<Eater>.ConstructEqualityHeuristic("BreakfastIsMade", 
            1f, e => e.Bread.Buttered && e.Bread.Toasted && e.Coffee.Made);

    [ExplicitVar] private static GoapVar<bool, Eater> _hasConsumedBreakfast 
        = BoolVar<Eater>.ConstructEqualityHeuristic("HasEatenBreakfast", 1f, e => e.Hungry == false);

    [SubGoal] private static GoapSubGoal<Eater> _subGoal 
        =  new DoBreakfastSubGoal(1f);
    public DoBreakfastGoal() : base(() => { })
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

