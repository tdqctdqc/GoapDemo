using Godot;
using System;

public class GoapVar<TValue, TAgent> : IGoapAgentVar<TAgent> where TValue : struct
{
    public string Name => _name;
    private readonly string _name;
    public Type ValueType => typeof(TValue);
    public readonly Func<TAgent, TValue> ValueFunc;
    protected GoapHeuristic<TValue, TAgent> _heuristic;
    protected GoapSatisfier<TAgent, TValue> _satisfier;
    
    

    public GoapVar(string name, Func<TAgent, TValue> valueFunc, 
                    GoapHeuristic<TValue, TAgent> heuristic,
                    GoapSatisfier<TAgent, TValue> satisfier)
    {
        _heuristic = heuristic;
        _satisfier = satisfier;
        ValueFunc = valueFunc;
        _name = name;
    }
    public static GoapVar<TValue,TAgent> ConstructEqualityHeuristic(string name, float missCost, Func<TAgent, TValue> valueFunc)
    {
        return new GoapVar<TValue,TAgent>(name, valueFunc, 
            GoapHeuristic<TValue, TAgent>.GetEqualityHeuristic(name, missCost), 
            GoapSatisfier<TAgent, TValue>.GetEqualitySatisfier());
    }
    public static GoapVar<TValue, TAgent> ConstructDistanceHeuristic(string name, float distCost, 
        Func<TValue, TValue, float> distFunc, Func<TAgent, TValue> valueFunc)
    {
        return new GoapVar<TValue, TAgent>(name, valueFunc, 
            GoapHeuristic<TValue, TAgent>.GetDistanceHeuristic(name, distCost, distFunc), 
            GoapSatisfier<TAgent, TValue>.GetEqualitySatisfier());
    }
    
    public bool SatisfiedBy(GoapFluent<TValue, TAgent> fluent, GoapState<TAgent> state)
    {
        return _satisfier.Check(fluent, state);
    }
    public float GetHeuristicCost(GoapFluent<TValue, TAgent> fluent, GoapState<TAgent> state)
    {
        return _heuristic.GetHeuristicCost(fluent.Value, state);
    }
    public GoapFluent<TValue, TAgent> Branch(TValue value)
    {
        return new GoapFluent<TValue, TAgent>(this, value);
    }
    public GoapFluent<TValue, TAgent> Branch(TAgent agent)
    {
        return new GoapFluent<TValue, TAgent>(this, ValueFunc(agent));
    }
    public IGoapAgentFluent<TAgent> BranchAgnosticByAgentEntity(TAgent entity)
    {
        return Branch(entity);
    }

    
    
    
}
