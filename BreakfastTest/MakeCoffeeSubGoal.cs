using Godot;
using System;

namespace GoapDemo.BreakfastTest
{
    public class MakeCoffeeSubGoal : GoapSubGoal<Eater>
    {
        [AvailableAction] private static GoapAction<Eater> _makeCoffeeAction
            = new MakeCoffeeAction();
        [AgentRequirement] private static GoapAgentRequirement<Eater> _req
            = new GoapAgentRequirement<Eater>(s => 1f, a => 1f);
        
        private static Lazy<GoapVar<bool, Eater>> _coffeeIsMadeVarLazy  = new Lazy<GoapVar<bool, Eater>>(
                   () => BoolVar<Eater>.ConstructEqualityHeuristic("CoffeeIsMade", 1f, e => e.Coffee.Made));
        [TargetFluent] private static GoapFluent<bool, Eater> _coffeeIsMadeFluent
            = new GoapFluent<bool, Eater>(_coffeeIsMadeVarLazy.Value, true);
        
        public MakeCoffeeSubGoal() 
            : base(sg => { })
        {
        }
    }
}
