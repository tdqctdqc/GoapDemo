using Godot;
using System;

public class GoapVarInstance<TValue, TAgent> : IGoapVarInstance where TValue : struct
{
    public GoapVar<TValue,TAgent> BaseVar { get; private set; }
    public TValue Value { get; private set; }
    public IGoapVar BaseVarGeneric => BaseVar;
    public Type ValueType => BaseVar.ValueType;
    public Type AgentType => BaseVar.AgentType;
    public string Name => BaseVar.Name;

    public GoapVarInstance(GoapVar<TValue,TAgent> baseVar, TValue value)
    {
        BaseVar = baseVar;
        Value = value; 
    }
    public GoapVarInstance(GoapVar<TValue,TAgent> baseVar, TAgent agent)
    {
        BaseVar = baseVar;
        Value = baseVar.ValueFunc(agent); 
    }
    public float GetHeuristicCost(IGoapVarInstance comparison)
    {
        return BaseVar.GetHeuristicCost(this, comparison);
    }

    public bool SatisfiedBy(IGoapState state)
    {
        return BaseVar.SatisfiedBy(this, state);
    }

    public object GetValue()
    {
        return Value;
    }
}