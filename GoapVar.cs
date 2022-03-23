using Godot;
using System;

public abstract class GoapVar<TValue, TAgent> : IGoapVar
{
    public TValue Value => _value;
    private readonly TValue _value;
    public string Name => _name;
    private readonly string _name;
    protected readonly Func<TAgent, TValue> _valueFunc;
    public Type ValueType { get; private set; }
    public GoapVar(string name, Func<TAgent, TValue> valueFunc, TAgent agent)
    {
        ValueType = typeof(TValue);
        _valueFunc = valueFunc;
        _name = name;
        _value = _valueFunc(agent);
    }
    public GoapVar(string name, Func<TAgent, TValue> valueFunc, TValue value)
    {
        ValueType = typeof(TValue);

        _valueFunc = valueFunc;
        _name = name;
        _value = value;
    }

    public abstract bool SatisfiedBy(IGoapState state);

    public abstract float GetHeuristicCost(IGoapVar comparison);

    public object GetValue()
    {
        return Value;
    }

    public abstract GoapVar<TValue, TAgent> Branch(TValue value);
    public abstract GoapVar<TValue, TAgent> Branch(TAgent agent);
    public abstract GoapVar<TValue, TAgent> Branch(GoapAgent<TAgent> agent);
}
