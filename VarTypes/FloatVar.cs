using Godot;
using System;

public class FloatVar<TAgent> : GoapVar<float, TAgent>
{
    private FloatVar(string name, Func<TAgent, float> valueFunc,
        GoapHeuristic<float, TAgent> heuristic,
        GoapSatisfier<TAgent, float> satisfier) : base(name, valueFunc, heuristic, satisfier)
    {
    }
    public static FloatVar<TAgent> ConstructScaleHeuristic(string name, float distCost, 
        Func<TAgent, float> valueFunc)
    {
        return new FloatVar<TAgent>(name, valueFunc, 
            GetDistanceHeuristic(name, Mathf.Inf, distCost, (f,g) => Mathf.Abs(f - g)), 
            EqualitySatisfier);
    }
}
