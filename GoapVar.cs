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

    public static GoapSatisfactionFunc<TAgent, TValue> EqualitySatisfier { get; private set; }
        = GetEqualitySatisfier();

    public GoapVar(string name, Func<TAgent, TValue> valueFunc, 
                    Func<TValue, object, float> heuristicFunc,
                    GoapSatisfactionFunc<TAgent, TValue> satisfiedFunc)
    {
        _heuristicFunc = heuristicFunc;
        _satisfiedFunc = satisfiedFunc;
        ValueFunc = valueFunc;
        _name = name;
    }

    public bool SatisfiedBy(GoapFluent<TValue, TAgent> fluent, GoapState<TAgent> state)
    {
        return _satisfiedFunc.Check(fluent, state);
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

    private static GoapSatisfactionFunc<TAgent, TValue> GetEqualitySatisfier()
    {
        var satisfier = new GoapSatisfactionFunc<TAgent, TValue>();
        satisfier.AddFunc( (f, s) =>  s.CheckVarMatch(f.Name, f.GetValue()) );
        return satisfier;
    }
}
