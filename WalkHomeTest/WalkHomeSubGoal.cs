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
            = new (WalkHomeGoal.AtHome, true);

        [AgentRequirement] private static GoapAgentRequirement<Walker> _req
            = new (s => 1f, a => 1f); 
        public WalkHomeSubGoal()
            : base("WalkHome", sg => {})
        {
        }
    }
}
