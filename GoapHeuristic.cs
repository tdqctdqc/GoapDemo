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
    public float GetHeuristic(TValue value, GoapState<TAgent> state)
    {
        return _heuristicFunc(value, state);
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
