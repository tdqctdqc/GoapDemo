using Godot;
using System;
using System.Collections.Generic;

public class GoapSatisfactionFunc<TAgent, TValue> where TValue : struct
{
    private List<Func<GoapFluent<TValue, TAgent>, GoapState<TAgent>, bool>> _satisfactionFuncs;

    public GoapSatisfactionFunc()
    {
        _satisfactionFuncs = new List<Func<GoapFluent<TValue, TAgent>, GoapState<TAgent>, bool>>();
    }
    public void AddFunc(Func<GoapFluent<TValue, TAgent>, GoapState<TAgent>, bool> satisfactionFunc)
    {
        _satisfactionFuncs.Add(satisfactionFunc);
    }
    public void AddFunc(Func<GoapState<TAgent>, bool> satisfactionFunc)
    {
        _satisfactionFuncs.Add( (f, s) => satisfactionFunc(s) );
    }
    public bool Check(GoapFluent<TValue, TAgent> goalCondition, GoapState<TAgent> state)
    {
        for (int i = 0; i < _satisfactionFuncs.Count; i++)
        {
            if (_satisfactionFuncs[i](goalCondition, state) == false) return false;
        }
        return true;
    }
}
