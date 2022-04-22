using Godot;
using System;

public interface IGoapAgentFluent<TAgent> : IGoapFluent
{
    float GetHeuristicCost(GoapState<TAgent> state);
    string Name { get; }
    Type ValueType { get; }
    Type AgentType { get; }
    bool SatisfiedBy(GoapState<TAgent> state);
    object GetValue();
}
