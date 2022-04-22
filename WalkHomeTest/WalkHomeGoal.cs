using System;
using System.Collections.Generic;
using Godot;

namespace GoapDemo.WalkHomeTest
{
    public class WalkHomeGoal : GoapGoal<Walker>
    {
        [ExplicitVar] private static GoapVar<Vector2, Walker> _currentPosition
            = Vec2Var<Walker>.ConstructScaledHeuristic("CurrentPosition", 1f, w => w.CurrentPosition);
        [ExplicitVar] private static GoapVar<Vector2, Walker> _homePosition
            = Vec2Var<Walker>.ConstructScaledHeuristic("HomePosition", 1f, w => w.HomePosition);
        [ExplicitVar] private static GoapVar<bool, Walker> _leftFootForward
            = BoolVar<Walker>.Construct("LeftFootForward", 1f, w => w.LeftFootForward);
        [ExplicitVar] private static GoapVar<float, Walker> _strideLength
            = FloatVar<Walker>.ConstructScaleHeuristic("StrideLength", 1f, w => w.StrideLength);
        
        [ImplicitVar] private static GoapVar<bool, Walker> _atHome
            = new AtHomeVar(1f);

        [SubGoal] private static GoapSubGoal<Walker> _subGoal
            = new WalkHomeSubGoal(1f);
        public override float Priority(GoapAgent<Walker> agent)
        {
            return 1f; 
        }

        private static bool AtHomeSatisfiedFunc(GoapFluent<bool, Walker> value, GoapState<Walker> state)
        {
            var homePos = state.GetVar<Vector2>(_homePosition.Name).Value;
            var currentPos = state.GetVar<Vector2>(_currentPosition.Name).Value;
            return homePos == currentPos;
        }
        public override GoapState<Walker> GetInitialState(List<GoapAgent<Walker>> agents)
        {
            return GetInitialStateFirstAgentMethod(agents);
        }

        private class WalkHomeSubGoal : GoapSubGoal<Walker>
        {
        
            public WalkHomeSubGoal(float difficulty) : base(GetTargetState(), difficulty)
            {
            }

            public override List<GoapAction<Walker>> Actions => _actions;
            private List<GoapAction<Walker>> _actions;
            protected override void BuildActions()
            {
                _actions = new List<GoapAction<Walker>>
                {
                    new LeftStepAction(),
                    new RightStepAction()
                };
            }

            public override float GetAgentCapability(GoapAgent<Walker> agent)
            {
                return 1f;
            }

            private static GoapState<Walker> GetTargetState()
            {
                var targetState = new GoapState<Walker>
                (
                    new GoapFluent<bool, Walker>(_atHome, true)
                );
                return targetState;
            }
        }
        private class AtHomeVar : BoolVar<Walker>
        {
            private static GoapSatisfier<Walker, bool> _atHomeSatisfier = GetSatisfier();
            public AtHomeVar(float missCost) 
                : base("AtHome", w => w.CurrentPosition == w.HomePosition, 
                    GetFlatCostHeuristic("AtHome", 1f), _atHomeSatisfier)
            {
            }

            private static GoapSatisfier<Walker, bool> GetSatisfier()
            {
                var satisfier = new GoapSatisfier<Walker, bool>();
                satisfier.AddFunc(s =>
                {
                    var homePos = s.GetVar<Vector2>(_homePosition.Name).Value;
                    var currentPos = s.GetVar<Vector2>(_currentPosition.Name).Value;
                    return homePos == currentPos;  
                });
                return satisfier;
            }
        }
    }
}
