using Godot;
using System;

public class FloatVar<TAgent> : GoapVar<float, TAgent>
{
    private FloatVar(string name, Func<TAgent, float> valueFunc,
        Func<float, object, float> heuristicFunc,
        GoapSatisfactionFunc<TAgent, float> satisfiedFunc) : base(name, valueFunc, heuristicFunc, satisfiedFunc)
    {
    }
    public static FloatVar<TAgent> ConstructScaleHeuristic(string name, float distCost, 
        Func<TAgent, float> valueFunc)
    {
        return new FloatVar<TAgent>(name, valueFunc, 
            (a, b) => ScaledHeuristicCost(distCost, a, b), 
            SimpleSatisfactionFunc);
    }

    private static float ScaledHeuristicCost(float weight, float instance, object comparison)
    {
        if (comparison is float v == false) return Mathf.Inf;
        return weight * Mathf.Abs(v - instance);
    }
}
