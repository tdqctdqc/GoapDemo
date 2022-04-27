using System;
using System.Collections.Generic;
using Godot;

namespace GoapDemo.WalkHomeTest
{
    public class WalkHomeGoal : GoapGoal<Walker>
    {
        [ExplicitVar] public static GoapVar<Vector2, Walker> CurrentPosition { get; private set; }
            = GoapVar<Vector2, Walker>.ConstructDistanceHeuristic("CurrentPosition", 1f, (v,w) => v.DistanceTo(w), w => w.CurrentPosition);
        [ExplicitVar] public static GoapVar<Vector2, Walker> HomePosition
            = GoapVar<Vector2, Walker>.ConstructDistanceHeuristic("HomePosition", 1f, (v,w) => v.DistanceTo(w), w => w.HomePosition);
        [ExplicitVar] public static GoapVar<bool, Walker> LeftFootForward
            = GoapVar<bool,Walker>.ConstructEqualityHeuristic("LeftFootForward", 1f, w => w.LeftFootForward);
        [ExplicitVar] public static GoapVar<float, Walker> StrideLength
            = GoapVar<float, Walker>.ConstructDistanceHeuristic("StrideLength", 1f, (f,g) => Mathf.Abs(f - g), w => w.StrideLength);
        
        

        [SubGoal] private static GoapSubGoal<Walker> _subGoal
            = new WalkHomeSubGoal();
        [TestCase] private static IGoapGoal GetTestCase() => new WalkHomeGoal();

        public WalkHomeGoal() : base("WalkHome", g => { })
        {
            
        }
        public override float Priority(GoapAgent<Walker> agent)
        {
            return 1f; 
        }
        public override GoapState<Walker> GetInitialState(List<GoapAgent<Walker>> agents)
        {
            return GetInitialStateFirstAgentMethod(agents);
        }
    }
}

