using Godot;
using System;

public abstract class GoapVar<TValue, TAgent> : IGoapVar where TValue : struct
{
    public string Name => _name;
    private readonly string _name;
    public readonly Func<TAgent, TValue> ValueFunc;
    protected Func<GoapVarInstance<TValue, TAgent>, IGoapVarInstance, float> _heuristicFunc;
    protected Func<GoapVarInstance<TValue, TAgent>, IGoapState, bool> _satisfiedFunc;
    public Type ValueType => typeof(TValue);
    public Type AgentType => typeof(TAgent);
    

    public GoapVar(string name, Func<TAgent, TValue> valueFunc, 
                    Func<GoapVarInstance<TValue, TAgent>, IGoapVarInstance, float> heuristicFunc,
                    Func<GoapVarInstance<TValue, TAgent>, IGoapState, bool> satisfiedFunc)
    {
        _heuristicFunc = heuristicFunc;
        _satisfiedFunc = satisfiedFunc;
        ValueFunc = valueFunc;
        _name = name;
    }

    public bool SatisfiedBy(GoapVarInstance<TValue, TAgent> instance, IGoapState state)
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
    public IGoapVarInstance BranchGeneric(IGoapAgent agent)
    {
        if (agent.GetAgent() is TAgent a)
        {
            return Branch(a);
        }
        return null; 
    }

    public static bool SimpleSatisfied(IGoapVarInstance instance, IGoapState state)
    {
        var vari = state.GetVarGeneric(instance.BaseVarGeneric);
        if (vari.GetValue() is TValue f == false) return false;
        return f.Equals((TValue)instance.GetValue());
    }
}
