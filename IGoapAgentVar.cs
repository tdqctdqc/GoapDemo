using Godot;
using System;

public interface IGoapAgentVar<TAgent> : IGoapVar
{
    IGoapVarInstance BranchAgnostic(GoapAgent<TAgent> agent);
}
