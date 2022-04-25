using Godot;
using System;

namespace GoapDemo.BreakfastTest
{
    public class MakeToastSubGoal : GoapSubGoal<Eater>
    {
        [AgentRequirement] private static GoapAgentRequirement<Eater> _req
            = new GoapAgentRequirement<Eater>(s => 1f, a => 1f); 
        [AvailableAction] private static GoapAction<Eater> _toastBreadAction
            = new ToastBreadAction();
        [AvailableAction] private static GoapAction<Eater> _butterToastAction
            = new PutButterOnToastAction();

        private static Lazy<GoapVar<bool, Eater>> _breadIsToastedVarLazy  = new Lazy<GoapVar<bool, Eater>>(
                   () => BoolVar<Eater>.ConstructEqualityHeuristic("BreadIsToasted", 1f, e => e.Bread.Toasted));
        [TargetFluent] private static GoapFluent<bool, Eater> _breadIsToastedFluent
            = new GoapFluent<bool, Eater>(_breadIsToastedVarLazy.Value, true);
        
        private static Lazy<GoapVar<bool, Eater>> _breadIsButteredVarLazy = new Lazy<GoapVar<bool, Eater>>( 
                    () => BoolVar<Eater>.ConstructEqualityHeuristic("BreadIsButtered", 1f, e => e.Bread.Buttered));
        [TargetFluent] private static GoapFluent<bool, Eater> _breadIsButteredFluent
            = new GoapFluent<bool, Eater>(_breadIsButteredVarLazy.Value, true);
        
        public MakeToastSubGoal() 
            : base(sg => { })
        {
        }
    }
}