using Godot;
using System;
using System.Collections.Generic;

public class GoapSatisfier<TAgent, TValue> where TValue : struct
{
    private List<Func<GoapFluent<TValue, TAgent>, GoapState<TAgent>, bool>> _satisfactionFuncs;

    public GoapSatisfier()
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
    public static GoapSatisfier<TAgent, TValue> GetImplicitEqualitySatisfier<TExplicitValue>
        (GoapVar<TExplicitValue,TAgent> masterVar, params GoapVar<TExplicitValue,TAgent>[] explicitVars)
        where TExplicitValue : struct
    {
        var satisfier = new GoapSatisfier<TAgent, TValue>();
        satisfier.AddFunc(s =>
        {
            var value = s.GetFluent<TExplicitValue>(masterVar.Name).Value;

            for (int i = 0; i < explicitVars.Length; i++)
            {
                var testValue = s.GetFluent<TExplicitValue>(explicitVars[i].Name).Value;
                if (testValue.Equals(value) == false) return false;
            }

            return true;
        });
        return satisfier;
    }
}
