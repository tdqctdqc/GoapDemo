using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace GoapDemo.BreakfastTest
{
    public class ConsumeBreakfastGoal : GoapGoal<Eater>
    {
        [ExplicitVar] public static GoapVar<bool, Eater> Hungry 
            = GoapVar<bool,Eater>.ConstructEqualityHeuristic("Hungry", 1f, e => e.Hungry);
        [ExplicitVar] public static GoapVar<bool, Eater> Caffeinated 
            = GoapVar<bool,Eater>.ConstructEqualityHeuristic("Caffeinated", 1f, e => e.Caffeinated);
        
        [SubGoal] private static GoapSubGoal<Eater> _subGoal 
            = new ConsumeBreakfastSubGoal();
        [TestCase] private static IGoapGoal GetTestCase() => new ConsumeBreakfastGoal();
        public ConsumeBreakfastGoal() : base("ConsumeBreakfast", g => { })
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


