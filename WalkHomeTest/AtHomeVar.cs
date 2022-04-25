using Godot;
using System;

namespace GoapDemo.WalkHomeTest
{
    public class AtHomeVar : BoolVar<Walker>
    {
        [ExplicitVar] private static GoapVar<Vector2, Walker> _currentPosition => WalkHomeGoal.CurrentPosition;
        [ExplicitVar] private static GoapVar<Vector2, Walker> _homePosition => WalkHomeGoal.HomePosition;
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
