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
    
    public static BoolVar<TAgent> ConstructEqualityHeuristic(string name, float missCost, Func<TAgent, bool> valueFunc)
    {
        return new BoolVar<TAgent>(name, valueFunc, 
            GetEqualityHeuristic(name, missCost), 
            EqualitySatisfier);
    }

    public static BoolVar<TAgent> ConstructImplicitDistanceHeuristic<TExplicitValue>
        (string name, Func<TAgent, bool> valueFunc, GoapVar<TExplicitValue, TAgent> masterVar, Func<TExplicitValue, TExplicitValue, float> distFunc, 
            params GoapVar<TExplicitValue, TAgent>[] explicitVars)
        where TExplicitValue : struct
    {
        var heuristic = GoapHeuristic<bool, TAgent>.GetImplicitDistHeuristic(masterVar, distFunc, explicitVars);
        var satisfier = GoapSatisfier<TAgent, bool>.GetImplicitEqualitySatisfier(masterVar, explicitVars);
        return new BoolVar<TAgent>(name, valueFunc, heuristic, satisfier);
    }
}
