using System;
using System.Collections.Generic;
using Godot;

namespace GoapDemo.WalkHomeTest
{
    public class WalkHomeGoal : GoapGoal<Walker>
    {
        [ExplicitVar] private static GoapVar<Vector2, Walker> _currentPosition
            = Vec2Var<Walker>.ConstructDistanceHeuristic("CurrentPosition", 1f, w => w.CurrentPosition);
        [ExplicitVar] private static GoapVar<Vector2, Walker> _homePosition
            = Vec2Var<Walker>.ConstructDistanceHeuristic("HomePosition", 1f, w => w.HomePosition);
        [ExplicitVar] private static GoapVar<bool, Walker> _leftFootForward
            = BoolVar<Walker>.ConstructEqualityHeuristic("LeftFootForward", 1f, w => w.LeftFootForward);
        [ExplicitVar] private static GoapVar<float, Walker> _strideLength
            = FloatVar<Walker>.ConstructDistanceHeuristic("StrideLength", 1f, w => w.StrideLength);
        
        [ImplicitVar] private static GoapVar<bool, Walker> _atHome
            = new AtHomeVar();

        [SubGoal] private static GoapSubGoal<Walker> _subGoal
            = new WalkHomeSubGoal();

        public WalkHomeGoal() : base(() => { })
        {
            
        }
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
    }
    
    
    public class AtHomeVar : BoolVar<Walker>
    {
        [ExplicitVar] private static GoapVar<Vector2, Walker> _currentPosition
            = Vec2Var<Walker>.ConstructDistanceHeuristic("CurrentPosition", 1f, w => w.CurrentPosition);
        [ExplicitVar] private static GoapVar<Vector2, Walker> _homePosition
            = Vec2Var<Walker>.ConstructDistanceHeuristic("HomePosition", 1f, w => w.HomePosition);

        private static GoapSatisfier<Walker, bool> _atHomeSatisfier = GetAtHomeSatisfier();
        private static GoapHeuristic<bool, Walker> _atHomeHeuristic = GetAtHomeHeuristic(1f);
        public AtHomeVar() 
            : base("AtHome", w => w.CurrentPosition == w.HomePosition, 
                _atHomeHeuristic, _atHomeSatisfier)
        {
        }
        private static GoapHeuristic<bool, Walker> GetAtHomeHeuristic(float distCost)
        {
            Func<GoapState<Walker>, float> heuristicFunc = s =>
            {
                var homePos = s.GetVar<Vector2>(_homePosition.Name).Value;
                var currentPos = s.GetVar<Vector2>(_currentPosition.Name).Value;
                return homePos.DistanceTo(currentPos) * distCost;  
            };
            var heuristic = new GoapHeuristic<bool, Walker>(heuristicFunc);
            return heuristic;
        }
        private static GoapSatisfier<Walker, bool> GetAtHomeSatisfier()
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

