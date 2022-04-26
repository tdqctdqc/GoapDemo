using Godot;
using System;

namespace GoapDemo.WalkHomeTest
{
    public class AtHomeVar : BoolVar<Walker>
    {
        [ExplicitVar] private static GoapVar<Vector2, Walker> _currentPosition => WalkHomeGoal.CurrentPosition;
        [ExplicitVar] private static GoapVar<Vector2, Walker> _homePosition => WalkHomeGoal.HomePosition;

        private static GoapSatisfier<Walker, bool> _atHomeSatisfier
            = GoapSatisfier<Walker, bool>.GetImplicitEqualitySatisfier(_homePosition, _currentPosition);
        
        private static GoapHeuristic<bool, Walker> _atHomeHeuristic 
            = GoapHeuristic<bool, Walker>.GetImplicitDistHeuristic(_homePosition, (p,q) => p.DistanceTo(q), _currentPosition);
            
        public AtHomeVar() 
            : base("AtHome", w => w.CurrentPosition == w.HomePosition, 
                _atHomeHeuristic, _atHomeSatisfier)
        {
        }
        private static GoapHeuristic<bool, Walker> GetAtHomeHeuristic(float distCost)
        {
            Func<GoapState<Walker>, float> heuristicFunc = s =>
            {
                var homePos = s.GetFluent<Vector2>(_homePosition.Name).Value;
                var currentPos = s.GetFluent<Vector2>(_currentPosition.Name).Value;
                return homePos.DistanceTo(currentPos) * distCost;  
            };
            var heuristic = new GoapHeuristic<bool, Walker>(heuristicFunc);
            return heuristic;
        }
    }
}
