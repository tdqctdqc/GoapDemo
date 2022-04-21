using Godot;
using System;

public class GoapSatisfactionFunc<TAgent, TValue> where TValue : struct
{
    private Func<GoapFluent<TValue, TAgent>, GoapState<TAgent>, bool> _satisfactionFunc;

    public GoapSatisfactionFunc(Func<GoapFluent<TValue, TAgent>, GoapState<TAgent>, bool> satisfactionFunc)
    {
        _satisfactionFunc = satisfactionFunc;
    }

    public bool Check(GoapFluent<TValue, TAgent> goalCondition, GoapState<TAgent> state)
    {
        return _satisfactionFunc(goalCondition, state);
    }
}
