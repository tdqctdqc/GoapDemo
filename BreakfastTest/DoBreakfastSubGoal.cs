using Godot;
using System;

namespace GoapDemo.BreakfastTest
{
    public class DoBreakfastSubGoal : GoapSubGoal<Eater>
    {
        private static Lazy<GoapVar<bool, Eater>> _hasConsumedBreakfastVarLazy = new Lazy<GoapVar<bool, Eater>>(
            () => BoolVar<Eater>.ConstructEqualityHeuristic("HasEatenBreakfast", 1f, e => e.Hungry == false));
        [TargetFluent] private static GoapFluent<bool, Eater> _hasConsumedBreakfastFluent 
            = new GoapFluent<bool, Eater>(_hasConsumedBreakfastVarLazy.Value, true);
        
        [AgentRequirement] private static GoapAgentRequirement<Eater> _req
            = new GoapAgentRequirement<Eater>(s => 1f, a => 1f); 
        
        [AvailableAction] private static GoapAction<Eater> _makeBreakfastAction 
            = new MakeBreakfastAction();
        [AvailableAction] private static GoapAction<Eater> _consumeBreakfastAction 
            = new ConsumeBreakfastAction();

        public DoBreakfastSubGoal(float diff) 
            : base(sg => { })
        {
            
        }
    }
}
