using Godot;
using System;

namespace GoapDemo.BreakfastTest
{
    public class ConsumeBreakfastSubGoal : GoapSubGoal<Eater>
    {
        private static Lazy<BoolVar<Eater>> _hungryVarLazy = new Lazy<BoolVar<Eater>>(
                 () => BoolVar<Eater>.ConstructEqualityHeuristic("Hungry", 1f, e => e.Hungry));
        [TargetFluent] private static GoapFluent<bool, Eater> _hungryFluent 
            = new GoapFluent<bool, Eater>(_hungryVarLazy.Value, false);
        
        private static Lazy<BoolVar<Eater>> _caffeinatedVarLazy = new Lazy<BoolVar<Eater>>(
                    () => BoolVar<Eater>.ConstructEqualityHeuristic("Caffeinated", 1f, e => e.Caffeinated));
        [TargetFluent] private static GoapFluent<bool, Eater> _caffeinatedFluent 
            = new GoapFluent<bool, Eater>(_caffeinatedVarLazy.Value, true);
        
        [AgentRequirement] private static GoapAgentRequirement<Eater> _req 
            = new GoapAgentRequirement<Eater>(s => 1f, a => 1f);
        
        [AvailableAction] private static GoapAction<Eater> _eatToastAction 
            =  new EatToastAction();
        
        [AvailableAction] private static GoapAction<Eater> _drinkCoffeeAction 
            = new DrinkCoffeeAction();
        public ConsumeBreakfastSubGoal() 
            : base(sg => { })
        {
        }
    }
}