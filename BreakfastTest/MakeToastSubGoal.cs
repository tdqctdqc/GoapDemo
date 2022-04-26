using Godot;
using System;

namespace GoapDemo.BreakfastTest
{
    public class MakeToastSubGoal : GoapSubGoal<Eater>
    {
        [AgentRequirement] private static GoapAgentRequirement<Eater> _req = new (s => 1f, a => 1f); 

        [TargetFluent] private static GoapFluent<bool, Eater> _breadIsToastedFluent
            = new GoapFluent<bool, Eater>(MakeBreakfastGoal.BreadIsToasted, true);
        
        [TargetFluent] private static GoapFluent<bool, Eater> _breadIsButteredFluent
            = new GoapFluent<bool, Eater>(MakeBreakfastGoal.BreadIsButtered, true);
        [AvailableAction] private static GoapAction<Eater> _toastBreadAction
                    = new ToastBreadAction();
        [AvailableAction] private static GoapAction<Eater> _butterToastAction
                    = new PutButterOnToastAction();
        public MakeToastSubGoal() 
            : base(sg => { })
        {
        }
    }
}