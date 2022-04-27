using Godot;
using System;

public class GoapHeuristic<TValue, TAgent> where TValue : struct
{
    private Func<TValue, GoapState<TAgent>, float> _heuristicFunc;

    public GoapHeuristic(Func<TValue, GoapState<TAgent>, float> heuristicFunc)
    {
        _heuristicFunc = heuristicFunc;
    }
    public GoapHeuristic(Func<GoapState<TAgent>, float> heuristicFunc)
    {
        _heuristicFunc = (v, s) => heuristicFunc(s);
    }
    public float GetHeuristicCost(TValue value, GoapState<TAgent> state)
    {
        return _heuristicFunc(value, state);
    }

    public static GoapHeuristic<TValue, TAgent> GetEqualityHeuristic(Func<GoapState<TAgent>, TValue> getValueFunc, float missCost)
    {
        return new GoapHeuristic<TValue, TAgent>
        (
            (value, state) =>
            {
                bool match = value.Equals(getValueFunc(state));
                return match ? 0f : missCost;
            }
        );
    }
    public static GoapHeuristic<TValue, TAgent> GetEqualityHeuristic(string varName, float missCost)
    {
        Func<GoapState<TAgent>, TValue> getValueFunc = s => s.GetFluent<TValue>(varName).Value;
        return GetEqualityHeuristic(getValueFunc, missCost);
    }
    public static GoapHeuristic<TValue, TAgent> GetDistanceHeuristic
        (string varName, float distCost, Func<TValue, TValue, float> distFunc)
    {
        Func<GoapState<TAgent>, TValue> getValueFunc = state => state.GetFluent<TValue>(varName).Value;
        return GetDistanceHeuristic(getValueFunc, distFunc, distCost);
    }

    public static GoapHeuristic<TValue, TAgent> GetDistanceHeuristic
        (Func<GoapState<TAgent>, TValue> getValueFunc, Func<TValue, TValue, float> distFunc, float distCost)
    {
        Func<TValue, GoapState<TAgent>, float> heuristicFunc = (value, state) =>
        {
            var stateValue = getValueFunc(state);
            return distFunc(value, stateValue) * distCost;
        };
        return new GoapHeuristic<TValue, TAgent>(heuristicFunc);
    }
    
    
    
    
    
    public static GoapHeuristic<TValue, TAgent> GetImplicitDistHeuristic<TExplicitValue>(GoapVar<TExplicitValue,TAgent> masterVar,
        Func<TExplicitValue,TExplicitValue,float> distFunc, params GoapVar<TExplicitValue,TAgent>[] explicitVars )
        where TExplicitValue : struct
    {
        Func<GoapState<TAgent>, float> heuristicFunc = s =>
        {
            var value = s.GetFluent<TExplicitValue>(masterVar.Name).Value;
            var cost = 0f;
            for (int i = 0; i < explicitVars.Length; i++)
            {
                var testValue = s.GetFluent<TExplicitValue>(explicitVars[i].Name).Value;
                cost += distFunc(value, testValue);
            }
            return cost;
        };
        var heuristic = new GoapHeuristic<TValue, TAgent>(heuristicFunc);
        return heuristic;
    }
}
