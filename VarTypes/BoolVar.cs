using Godot;
using System;

public class BoolVar<TAgent> : GoapVar<bool,TAgent>
{
    
    protected BoolVar(string name, Func<TAgent, bool> valueFunc, 
        GoapHeuristic<bool, TAgent> heuristic,
        GoapSatisfier<TAgent, bool> satisfier) 
            : base(name, valueFunc, heuristic, satisfier)
    {
    }

    public static BoolVar<TAgent> Construct(string name, float missCost, Func<TAgent, bool> valueFunc)
    {
        return new BoolVar<TAgent>(name, valueFunc, 
            GetFlatCostHeuristic(name, missCost), 
            EqualitySatisfier);
    }
    
}
