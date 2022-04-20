using Godot;
using System;

public abstract class GoapVar<TValue, TAgent> : IGoapAgentVar<TAgent> where TValue : struct
{
    public string Name => _name;
    private readonly string _name;
    public Type ValueType => typeof(TValue);
    public readonly Func<TAgent, TValue> ValueFunc;
    protected Func<TValue, object, float> _heuristicFunc;
    protected Func<GoapVarInstance<TValue, TAgent>, GoapState<TAgent>, bool> _satisfiedFunc;
    

    public GoapVar(string name, Func<TAgent, TValue> valueFunc, 
                    Func<TValue, object, float> heuristicFunc,
                    Func<GoapVarInstance<TValue, TAgent>, GoapState<TAgent>, bool> satisfiedFunc)
    {
        _heuristicFunc = heuristicFunc;
        _satisfiedFunc = satisfiedFunc;
        ValueFunc = valueFunc;
        _name = name;
    }

    public bool SatisfiedBy(GoapVarInstance<TValue, TAgent> instance, GoapState<TAgent> state)
    {
        return _satisfiedFunc(instance, state);
    }
    public float GetHeuristicCost(GoapVarInstance<TValue, TAgent> instance, object comparison)
    {
        return _heuristicFunc(instance.Value, comparison);
    }
    public GoapVarInstance<TValue, TAgent> Branch(TValue value)
    {
        return new GoapVarInstance<TValue, TAgent>(this, value);
    }
    public GoapVarInstance<TValue, TAgent> Branch(TAgent agent)
    {
        return new GoapVarInstance<TValue, TAgent>(this, ValueFunc(agent));
    }
    public IGoapAgentVarInstance<TAgent> BranchAgnosticByAgentEntity(TAgent entity)
    {
        return Branch(entity);
    }

    public static bool SimpleSatisfied(IGoapAgentVarInstance<TAgent> instance, GoapState<TAgent> state)
    {
        return state.CheckVarMatch(instance.Name, instance.GetValue());
    }
}
