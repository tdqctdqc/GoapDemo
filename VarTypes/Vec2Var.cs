using Godot;
using System;

public class Vec2Var<TAgent> : GoapVar<Vector2, TAgent>
{
    private float _distCost;
    public Vec2Var(string name, float distCost, Func<TAgent, Vector2> valueFunc, TAgent agent) : base(name, valueFunc, agent)
    {
        _distCost = distCost;
    }

    public Vec2Var(string name, float distCost, Func<TAgent, Vector2> valueFunc, Vector2 value) : base(name, valueFunc, value)
    {
        _distCost = distCost;
    }


    public override bool SatisfiedBy(IGoapState state)
    {
        var vari = state.GetGenericVar(this);
        if (vari.GetValue() is Vector2 v == false) return false;
        return v == Value;
    }

    public override float GetHeuristicCost(IGoapVar comparison)
    {
        if (comparison.GetValue() is Vector2 v == false) return Mathf.Inf;
        return _distCost * v.DistanceTo(Value);
    }

    public override GoapVar<Vector2, TAgent> Branch(Vector2 value)
    {
        return new Vec2Var<TAgent>(Name, _distCost, _valueFunc, value);
    }

    public override GoapVar<Vector2, TAgent> Branch(TAgent agent)
    {
        return new Vec2Var<TAgent>(Name, _distCost, _valueFunc, agent);

    }

    public override GoapVar<Vector2, TAgent> Branch(GoapAgent<TAgent> agent)
    {
        return new Vec2Var<TAgent>(Name, _distCost, _valueFunc, agent.Agent);
    }
}
