using Godot;
using System;

public class GoapVarInstance<TValue, TAgent> : IGoapAgentVarInstance<TAgent> where TValue : struct
{
    public GoapVar<TValue,TAgent> BaseVar { get; private set; }
    public TValue Value { get; private set; }
    public Type ValueType => typeof(TValue);
    public Type AgentType => typeof(TAgent);
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
    public float GetHeuristicCost(IGoapAgentVarInstance<TAgent> comparison)
    {
        return BaseVar.GetHeuristicCost(this, comparison);
    }

    public bool SatisfiedBy(IGoapState state)
    {
        if (state is GoapState<TAgent> s == false) return false;
        return BaseVar.SatisfiedBy(this, s);
    }

    public object GetValue()
    {
        return Value;
    }
}
