using Godot;
using System;

public interface IGoapVarInstance
{
    string Name { get; }
    IGoapVar BaseVarGeneric { get; }
    Type ValueType { get; }
    Type AgentType { get; }
    float GetHeuristicCost(IGoapVarInstance comparison);
    bool SatisfiedBy(IGoapState state);
    object GetValue();
}
