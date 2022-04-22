using Godot;
using System;

public class GoapFluent<TValue, TAgent> : IGoapAgentFluent<TAgent> where TValue : struct
{
    public GoapVar<TValue, TAgent> BaseVar { get; private set; }
    public TValue Value { get; private set; }
    public Type ValueType => typeof(TValue);
    public Type AgentType => typeof(TAgent);
    public string Name => BaseVar.Name;

    public GoapFluent(GoapVar<TValue,TAgent> baseVar, TValue value)
    {
        BaseVar = baseVar;
        Value = value; 
    }
    public GoapFluent(GoapVar<TValue,TAgent> baseVar, TAgent agent)
    {
        BaseVar = baseVar;
        Value = baseVar.ValueFunc(agent); 
    }
    public float GetHeuristicCost(GoapState<TAgent> state)
    {
        return BaseVar.GetHeuristicCost(this, state);
    }
    public bool SatisfiedBy(GoapState<TAgent> state)
    {
        return BaseVar.SatisfiedBy(this, state);
    }

    public object GetValue()
    {
        return Value;
    }
}
