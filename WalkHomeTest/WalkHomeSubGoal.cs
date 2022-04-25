using Godot;
using System;

namespace GoapDemo.WalkHomeTest
{
    public class WalkHomeSubGoal : GoapSubGoal<Walker>
    {
        [AvailableAction] private static GoapAction<Walker> _leftStepAction
            = new LeftStepAction();
        [AvailableAction] private static GoapAction<Walker> _rightStepAction
            = new RightStepAction();
        [TargetFluent] private static GoapFluent<bool, Walker> _atHomeFluent
            = new GoapFluent<bool, Walker>(new AtHomeVar(), true);

        [AgentRequirement] private static GoapAgentRequirement<Walker> _req
            = new GoapAgentRequirement<Walker>(s => 1f, a => 1f); 
        public WalkHomeSubGoal()
            : base(sg => {})
        {
        }
    }
}
