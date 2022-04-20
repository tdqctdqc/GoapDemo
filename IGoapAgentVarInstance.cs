using Godot;
using System;

public interface IGoapAgentVarInstance<TAgent> : IGoapVarInstance
{
    float GetHeuristicCost(IGoapAgentVarInstance<TAgent> comparison);
    float GetHeuristicCost(object comparison);
    string Name { get; }
    Type ValueType { get; }
    Type AgentType { get; }
    bool SatisfiedBy(GoapState<TAgent> state);
    object GetValue();
}
