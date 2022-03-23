using Godot;
using System;

public class FloatVar<TAgent> : GoapVar<float, TAgent>
{
    private float _distCost;

    public FloatVar(string name, float distCost, Func<TAgent, float> valueFunc, TAgent agent) : base(name, valueFunc, agent)
    {
        _distCost = distCost;
    }
    
    public FloatVar(string name, float distCost, Func<TAgent, float> valueFunc, float value) : base(name, valueFunc, value)
    {
        _distCost = distCost;
    }

    public override bool SatisfiedBy(IGoapState state)
    {
        var vari = state.GetGenericVar(this);
        if (vari.GetValue() is float f == false) return false;
        return f == Value;
    }
    public override float GetHeuristicCost(IGoapVar comparison)
    {
        if (comparison.GetValue() is float v == false) return Mathf.Inf;
    
        return _distCost * Mathf.Abs(v - Value);
    }

    public override GoapVar<float, TAgent> Branch(float value)
    {
        return new FloatVar<TAgent>(Name, _distCost, _valueFunc, value);
    }

    public override GoapVar<float, TAgent> Branch(TAgent agent)
    {
        return new FloatVar<TAgent>(Name, _distCost, _valueFunc, agent);

    }

    public override GoapVar<float, TAgent> Branch(GoapAgent<TAgent> agent)
    {
        return new FloatVar<TAgent>(Name, _distCost, _valueFunc, agent.Agent);
    }
}
