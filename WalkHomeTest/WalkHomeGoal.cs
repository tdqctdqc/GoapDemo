using System;
using System.Collections.Generic;
using Godot;

namespace GoapDemo.WalkHomeTest
{
    public class WalkHomeGoal : GoapGoal<Walker>
    {
        [ExplicitVar] public static GoapVar<Vector2, Walker> CurrentPosition { get; private set; }
            = Vec2Var<Walker>.ConstructDistanceHeuristic("CurrentPosition", 1f, w => w.CurrentPosition);
        [ExplicitVar] public static GoapVar<Vector2, Walker> HomePosition
            = Vec2Var<Walker>.ConstructDistanceHeuristic("HomePosition", 1f, w => w.HomePosition);
        [ExplicitVar] public static GoapVar<bool, Walker> LeftFootForward
            = BoolVar<Walker>.ConstructEqualityHeuristic("LeftFootForward", 1f, w => w.LeftFootForward);
        [ExplicitVar] public static GoapVar<float, Walker> StrideLength
            = FloatVar<Walker>.ConstructDistanceHeuristic("StrideLength", 1f, w => w.StrideLength);
        
        [ImplicitVar] public static GoapVar<bool, Walker> AtHome
            = BoolVar<Walker>.ConstructImplicitDistanceHeuristic<Vector2>("AtHome",
        e => e.CurrentPosition == e.HomePosition,
        HomePosition, (p, q) => p.DistanceTo(q), CurrentPosition);

        [SubGoal] private static GoapSubGoal<Walker> _subGoal
            = new WalkHomeSubGoal();
        [TestCase] private static IGoapGoal GetTestCase() => new WalkHomeGoal();

        public WalkHomeGoal() : base(g => { })
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

