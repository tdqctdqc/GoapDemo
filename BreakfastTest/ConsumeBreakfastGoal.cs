using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace GoapDemo.BreakfastTest
{
    public class ConsumeBreakfastGoal : GoapGoal<Eater>
    {
        [ExplicitVar] private static GoapVar<bool, Eater> _hungry 
            = BoolVar<Eater>.ConstructEqualityHeuristic("Hungry", 1f, e => e.Hungry);

        [ExplicitVar] private static GoapVar<bool, Eater> _caffeinated 
            = BoolVar<Eater>.ConstructEqualityHeuristic("Caffeinated", 1f, e => e.Caffeinated);
        
        [SubGoal] private static GoapSubGoal<Eater> _subGoal 
            = new ConsumeBreakfastSubGoal();
        
        public ConsumeBreakfastGoal() : base(() => { })
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


