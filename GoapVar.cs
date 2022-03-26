using Godot;
using System;

public abstract class GoapVar<TValue, TAgent> : IGoapAgentVar<TAgent> where TValue : struct
{
    public string Name => _name;
    private readonly string _name;
    public readonly Func<TAgent, TValue> ValueFunc;
    protected Func<GoapVarInstance<TValue, TAgent>, IGoapVarInstance, float> _heuristicFunc;
    protected Func<GoapVarInstance<TValue, TAgent>, GoapState<TAgent>, bool> _satisfiedFunc;
    public Type ValueType => typeof(TValue);
    public Type AgentType => typeof(TAgent);
    

    public GoapVar(string name, Func<TAgent, TValue> valueFunc, 
                    Func<GoapVarInstance<TValue, TAgent>, IGoapVarInstance, float> heuristicFunc,
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

    public float GetHeuristicCost(GoapVarInstance<TValue, TAgent> instance, IGoapVarInstance comparison)
    {
        return _heuristicFunc(instance, comparison);
    }
    
    public GoapVarInstance<TValue, TAgent> Branch(TValue value)
    {
        return new GoapVarInstance<TValue, TAgent>(this, value);
    }
    
    public GoapVarInstance<TValue, TAgent> Branch(TAgent agent)
    {
        return new GoapVarInstance<TValue, TAgent>(this, ValueFunc(agent));
    }
    public IGoapVarInstance BranchGeneric(GoapAgent<TAgent> agent)
    {
        return Branch(agent.Entity);
    }

    public static bool SimpleSatisfied(IGoapVarInstance instance, GoapState<TAgent> state)
    {
        var vari = state.GetVarTypeChecked(instance.Name, instance.ValueType);
        if (vari.GetValue() is TValue f == false) return false;
        return f.Equals((TValue)instance.GetValue());
    }
}
