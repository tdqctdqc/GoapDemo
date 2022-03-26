using Godot;
using System;

public interface IGoapAgentVar<TAgent> : IGoapVar
{
    IGoapVarInstance BranchGeneric(GoapAgent<TAgent> agent);
}
