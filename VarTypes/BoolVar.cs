using Godot;
using System;

public class BoolVar<TAgent> : GoapVar<bool,TAgent>
{
    protected BoolVar(string name, Func<TAgent, bool> valueFunc, 
        Func<bool, object, float> heuristicFunc,
        GoapSatisfactionFunc<TAgent, bool> satisfiedFunc) 
            : base(name, valueFunc, heuristicFunc, satisfiedFunc)
    {
    }

    public static BoolVar<TAgent> Construct(string name, float missCost, Func<TAgent, bool> valueFunc)
    {
        return new BoolVar<TAgent>(name, valueFunc, 
            (a, b) => FlatCostHeuristic(missCost, a, b), 
            SimpleSatisfactionFunc);
    }
    public static BoolVar<TAgent> ConstructCustomSatisfiedFunc(string name, float missCost, 
        Func<TAgent, bool> valueFunc, 
        GoapSatisfactionFunc<TAgent, bool> satisfiedFunc)
    {
        return new BoolVar<TAgent>(name, valueFunc, 
            (a, b) => FlatCostHeuristic(missCost, a, b), 
            satisfiedFunc);
    }
    public static float FlatCostHeuristic(float missHeurCost, bool instance, object comparison)
    {
        if (comparison == null) return missHeurCost; 
        if (comparison is bool b == false) return missHeurCost;
        return b == instance ? 0f : missHeurCost;
    }
}
