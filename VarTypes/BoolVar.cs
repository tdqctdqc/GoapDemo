using Godot;
using System;

public class BoolVar<TAgent> : GoapVar<bool,TAgent>
{
    private BoolVar(string name, Func<TAgent, bool> valueFunc, 
        Func<GoapVarInstance<bool, TAgent>, IGoapAgentVarInstance<TAgent>, float> heuristicFunc,
        Func<GoapVarInstance<bool, TAgent>, GoapState<TAgent>, bool> satisfiedFunc) 
            : base(name, valueFunc, heuristicFunc, satisfiedFunc)
    {
    }

    public static BoolVar<TAgent> Construct(string name, float missCost, Func<TAgent, bool> valueFunc)
    {
        return new BoolVar<TAgent>(name, valueFunc, 
            (a, b) => FlatCostHeuristic(missCost, a, b), 
            SimpleSatisfied);
    }

    public static float FlatCostHeuristic(float missHeurCost, GoapVarInstance<bool,TAgent> instance, IGoapAgentVarInstance<TAgent> comparison)
    {
        if (comparison == null) return missHeurCost; 
        if (comparison.GetValue() is bool b == false) return missHeurCost;
        return b == instance.Value ? 0f : missHeurCost;
    }
}
