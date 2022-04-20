using Godot;
using System;

public class FloatVar<TAgent> : GoapVar<float, TAgent>
{
    private FloatVar(string name, Func<TAgent, float> valueFunc,
        Func<GoapVarInstance<float, TAgent>, IGoapAgentVarInstance<TAgent>, float> heuristicFunc,
        Func<GoapVarInstance<float, TAgent>, GoapState<TAgent>, bool> satisfiedFunc) : base(name, valueFunc, heuristicFunc, satisfiedFunc)
    {
    }
    public static FloatVar<TAgent> ConstructScaleHeuristic(string name, float distCost, 
        Func<TAgent, float> valueFunc)
    {
        return new FloatVar<TAgent>(name, valueFunc, 
            (a, b) => ScaledHeuristicCost(distCost, a, b), 
            SimpleSatisfied);
    }

    private static float ScaledHeuristicCost(float weight, GoapVarInstance<float,TAgent> instance, IGoapAgentVarInstance<TAgent> comparison)
    {
        if (comparison.GetValue() is float v == false) return Mathf.Inf;
        return weight * Mathf.Abs(v - instance.Value);
    }
}
