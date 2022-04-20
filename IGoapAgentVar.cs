using Godot;
using System;

public interface IGoapAgentVar<TAgent> : IGoapVar
{
    IGoapAgentVarInstance<TAgent> BranchAgnosticByAgentEntity(TAgent agent);
}
