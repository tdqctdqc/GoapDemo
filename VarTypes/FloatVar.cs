using Godot;
using System;

public class FloatVar<TAgent> : GoapVar<float, TAgent>
{
    
    private FloatVar(string name, Func<TAgent, float> valueFunc,
        Func<GoapVarInstance<float, TAgent>, IGoapVarInstance, float> heuristicFunc,
        Func<GoapVarInstance<float, TAgent>, IGoapState, bool> satisfiedFunc) : base(name, valueFunc, heuristicFunc, satisfiedFunc)
    {
    }
    public static FloatVar<TAgent> ConstructScaleHeuristic(string name, float distCost, 
        Func<TAgent, float> valueFunc)
    {
        return new FloatVar<TAgent>(name, valueFunc, 
            (a, b) => ScaledHeuristicCost(distCost, a, b), 
            SimpleSatisfied);
    }

    public static float ScaledHeuristicCost(float weight, GoapVarInstance<float,TAgent> instance, IGoapVarInstance comparison)
    {
        if (comparison.GetValue() is float v == false) return Mathf.Inf;
        return weight * Mathf.Abs(v - instance.Value);
    }
}