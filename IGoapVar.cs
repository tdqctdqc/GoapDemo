using Godot;
using System;

public interface IGoapVar 
{
    string Name { get; }
    Type ValueType { get; }
    Type AgentType { get; }
}
