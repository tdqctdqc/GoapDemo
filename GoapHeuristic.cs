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
}
