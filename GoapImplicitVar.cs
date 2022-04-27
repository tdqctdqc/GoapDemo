using Godot;
using System;
using System.Runtime.Remoting.Messaging;

public class GoapImplicitVar<TValue, TAgent> : IGoapImplicitVar where TValue : struct
{
    private GoapHeuristic<TValue, TAgent> _heuristic;
    private GoapSatisfier<TAgent, TValue> _satisfier;

    private GoapImplicitVar(Func<GoapState<TAgent>, TValue> getValueFunc,
        GoapHeuristic<TValue, TAgent> heuristic, GoapSatisfier<TAgent, TValue> satisfier)
    {
        _heuristic = heuristic;
        _satisfier = satisfier;
    }

    public static GoapImplicitVar<TValue, TAgent> ConstructEqualityHeuristic(
        Func<GoapState<TAgent>, TValue> getValueFunc, float missCost)
    {
        Func<TValue, GoapState<TAgent>, float> heuristicFunc = (value, state) =>
        {
            var stateValue = getValueFunc(state);
            return stateValue.Equals(value) ? 0f : missCost;
        };
        var heuristic = new GoapHeuristic<TValue, TAgent>(heuristicFunc);
            // GoapHeuristic<TValue, TAgent>.GetEqualityHeuristic(getValueFunc, missCost);
        var satisfier = GoapSatisfier<TAgent, TValue>.GetEqualitySatisfier(getValueFunc);
        return new GoapImplicitVar<TValue, TAgent>(getValueFunc, heuristic, satisfier);
    }
    
    public static GoapImplicitVar<TValue, TAgent> ConstructDistanceHeuristic(
        Func<GoapState<TAgent>, TValue> getValueFunc, Func<GoapState<TAgent>,float> distFunc, float distCost, float margin)
    {
        var heuristic = new GoapHeuristic<TValue, TAgent>(s => distFunc(s) * distCost);
        var satisfier = new GoapSatisfier<TAgent, TValue>();
        Func<GoapState<TAgent>, bool> satisfiedFunc = state =>
        {
            return distFunc(state) <= margin;
        };
        satisfier.AddFunc(satisfiedFunc);
        return new GoapImplicitVar<TValue, TAgent>(getValueFunc, heuristic, satisfier);
    }
}

public interface IGoapImplicitVar
{
    
}
