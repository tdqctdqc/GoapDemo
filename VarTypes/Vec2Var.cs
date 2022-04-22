using Godot;
using System;

public class Vec2Var<TAgent> : GoapVar<Vector2, TAgent>
{
    private Vec2Var(string name, Func<TAgent, Vector2> valueFunc, 
        GoapHeuristic<Vector2, TAgent> heuristic,
        GoapSatisfier<TAgent, Vector2> satisfier) : base(name, valueFunc, heuristic, satisfier)
    {
    }

    public static Vec2Var<TAgent> ConstructScaledHeuristic(string name, float distCost, Func<TAgent, Vector2> valueFunc)
    {
        return new Vec2Var<TAgent>(name, valueFunc, 
            GetDistanceHeuristic(name, Mathf.Inf, distCost, (v,w) => v.DistanceTo(w)), 
            EqualitySatisfier);
    }
}
