using Godot;
using System;

public interface IGoapAgent
{
    IGoapVarInstance[] GetBranchedVars();
    Type AgentType { get; }
    object GetAgent();
}
