using Godot;
using System;

public interface IGoapVar 
{
    string Name { get; }
    float GetHeuristicCost(IGoapVar comparison);
    object GetValue();
    Type ValueType { get; }
    bool SatisfiedBy(IGoapState state);
}
