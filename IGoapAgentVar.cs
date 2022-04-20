using Godot;
using System;

public interface IGoapAgentVar<TAgent> : IGoapVar
{
    IGoapAgentVarInstance<TAgent> BranchAgnosticByAgent(GoapAgent<TAgent> agent);
    IGoapAgentVarInstance<TAgent> BranchAgnosticByValue(object value);
}
