using Godot;
using System;

public interface IGoapAgentVarInstance<TAgent> : IGoapVarInstance
{
    float GetHeuristicCost(IGoapAgentVarInstance<TAgent> comparison);
    string Name { get; }
    Type ValueType { get; }
    Type AgentType { get; }
    bool SatisfiedBy(IGoapState state);
    object GetValue();
}
