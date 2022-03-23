using Godot;
using System;

public class BoolVar<TAgent> : GoapVar<bool,TAgent>
{
    public BoolVar(string name, Func<TAgent, bool> valueFunc, TAgent agent) : base(name, valueFunc, agent)
    {
    }

    public BoolVar(string name, Func<TAgent, bool> valueFunc, bool value) : base(name, valueFunc, value)
    {
    }

    public override bool SatisfiedBy(IGoapState state)
    {
        var vari = state.GetGenericVar(this);
        if (vari.GetValue() is bool b == false) return false;
        return b == Value;
    }

    public override float GetHeuristicCost(IGoapVar comparison)
    {
        if (comparison.GetValue() is bool b == false) return Mathf.Inf;
        return b == Value ? 0f : Mathf.Inf;
    }

    public override GoapVar<bool, TAgent> Branch(bool value)
    {
        return new BoolVar<TAgent>(Name, _valueFunc, value);
    }
    public override GoapVar<bool, TAgent> Branch(TAgent agent)
    {
        return new BoolVar<TAgent>(Name, _valueFunc, agent);
    }
    public override GoapVar<bool, TAgent> Branch(GoapAgent<TAgent> agent)
    {
        return new BoolVar<TAgent>(Name, _valueFunc, agent.Agent);
    }
}
