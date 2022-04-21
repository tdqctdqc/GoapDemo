using Godot;
using System;

public interface IGoapAgentVar<TAgent> : IGoapVar
{
    IGoapAgentFluent<TAgent> BranchAgnosticByAgentEntity(TAgent agent);
}
