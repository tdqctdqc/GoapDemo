using Godot;
using System;

public abstract class GoapVar<TValue, TAgent> : IGoapAgentVar<TAgent> where TValue : struct
{
    public string Name => _name;
    private readonly string _name;
    public Type ValueType => typeof(TValue);
    public readonly Func<TAgent, TValue> ValueFunc;
    protected GoapHeuristic<TValue, TAgent> _heuristic;
    protected GoapSatisfier<TAgent, TValue> _satisfier;
    
    public static GoapSatisfier<TAgent, TValue> EqualitySatisfier { get; private set; }
        = GetEqualitySatisfier();

    public GoapVar(string name, Func<TAgent, TValue> valueFunc, 
                    GoapHeuristic<TValue, TAgent> heuristic,
                    GoapSatisfier<TAgent, TValue> satisfier)
    {
        _heuristic = heuristic;
        _satisfier = satisfier;
        ValueFunc = valueFunc;
        _name = name;
    }

    public bool SatisfiedBy(GoapFluent<TValue, TAgent> fluent, GoapState<TAgent> state)
    {
        return _satisfier.Check(fluent, state);
    }
    public float GetHeuristicCost(GoapFluent<TValue, TAgent> fluent, GoapState<TAgent> state)
    {
        return _heuristic.GetHeuristic(fluent.Value, state);
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

    private static GoapSatisfier<TAgent, TValue> GetEqualitySatisfier()
    {
        var satisfier = new GoapSatisfier<TAgent, TValue>();
        satisfier.AddFunc( (f, s) =>  s.CheckVarMatch(f.Name, f.Value) );
        return satisfier;
    }
    public static GoapHeuristic<TValue, TAgent> GetEqualityHeuristic(string varName, float missCost)
    {
        Func<TValue, GoapState<TAgent>, float> heuristicFunc = (value, state) =>
        {
            var stateFluent = state.GetFluent<TValue>(varName);
            if (stateFluent == null) return missCost; 
            if (stateFluent.Value is TValue v == false) return missCost;
            return v.Equals(value) ? 0f : missCost;
        };
        return new GoapHeuristic<TValue, TAgent>(heuristicFunc);
    }
    public static GoapHeuristic<TValue, TAgent> GetDistanceHeuristic
        (string varName, float missHeurCost, float distCost, Func<TValue, TValue, float> distFunc)
    {
        Func<TValue, GoapState<TAgent>, float> heuristicFunc = (value, state) =>
        {
            var stateFluent = state.GetFluent<TValue>(varName);
            if (stateFluent == null) return missHeurCost; 
            if (stateFluent.Value is TValue v == false) return missHeurCost;
            return distFunc(value, stateFluent.Value);
        };
        return new GoapHeuristic<TValue, TAgent>(heuristicFunc);
    }
}
