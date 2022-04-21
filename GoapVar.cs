using Godot;
using System;

public abstract class GoapVar<TValue, TAgent> : IGoapAgentVar<TAgent> where TValue : struct
{
    public string Name => _name;
    private readonly string _name;
    public Type ValueType => typeof(TValue);
    public readonly Func<TAgent, TValue> ValueFunc;
    protected Func<TValue, object, float> _heuristicFunc;
    protected GoapSatisfactionFunc<TAgent, TValue> _satisfiedFunc;
    public static GoapSatisfactionFunc<TAgent, TValue> SimpleSatisfactionFunc =
        new GoapSatisfactionFunc<TAgent, TValue>(SimpleSatisfied);

    public GoapVar(string name, Func<TAgent, TValue> valueFunc, 
                    Func<TValue, object, float> heuristicFunc,
                    GoapSatisfactionFunc<TAgent, TValue> satisfiedFunc)
    {
        _heuristicFunc = heuristicFunc;
        _satisfiedFunc = satisfiedFunc;
        ValueFunc = valueFunc;
        _name = name;
    }

    public bool SatisfiedBy(GoapFluent<TValue, TAgent> instance, GoapState<TAgent> state)
    {
        return _satisfiedFunc.Check(instance, state);
    }
    public float GetHeuristicCost(GoapFluent<TValue, TAgent> instance, object comparison)
    {
        return _heuristicFunc(instance.Value, comparison);
    }
    public GoapFluent<TValue, TAgent> Branch(TValue value)
    {
        return new GoapFluent<TValue, TAgent>(this, value);
    }
    public GoapFluent<TValue, TAgent> Branch(TAgent agent)
    {
        return new GoapFluent<TValue, TAgent>(this, ValueFunc(agent));
    }
    public IGoapAgentFluent<TAgent> BranchAgnosticByAgentEntity(TAgent entity)
    {
        return Branch(entity);
    }

    private static bool SimpleSatisfied(IGoapAgentFluent<TAgent> instance, GoapState<TAgent> state)
    {
        return state.CheckVarMatch(instance.Name, instance.GetValue());
    }

    
}
