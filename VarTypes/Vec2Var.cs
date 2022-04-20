using Godot;
using System;

public class Vec2Var<TAgent> : GoapVar<Vector2, TAgent>
{
    private Vec2Var(string name, Func<TAgent, Vector2> valueFunc, 
        Func<Vector2, object, float> heuristicFunc,
        Func<GoapVarInstance<Vector2, TAgent>, GoapState<TAgent>, bool> satisfiedFunc) : base(name, valueFunc, heuristicFunc, satisfiedFunc)
    {
    }

    public static Vec2Var<TAgent> ConstructScaledHeuristic(string name, float distCost, Func<TAgent, Vector2> valueFunc)
    {
        return new Vec2Var<TAgent>(name, valueFunc, 
            (a, b) => ScaledHeuristicCost(distCost, a, b), 
            SimpleSatisfied);
    }

    public static float ScaledHeuristicCost(float scale, Vector2 instance, object comparison)
    {
        if (comparison is Vector2 v == false) return Mathf.Inf;
        return scale * v.DistanceTo(instance);
    }
}
