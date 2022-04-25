using Godot;
using System;

public class GoapAgentRequirement<TAgent>
{
    public Func<GoapState<TAgent>, float> NeedMagnitudeFunc { get; private set; }
    public Func<GoapAgent<TAgent>, float> AgentCapabilityFunc { get; private set; }

    public GoapAgentRequirement(Func<GoapState<TAgent>, float> needMagnitudeFunc, Func<GoapAgent<TAgent>, float> agentCapabilityFunc)
    {
        NeedMagnitudeFunc = needMagnitudeFunc;
        AgentCapabilityFunc = agentCapabilityFunc;
    }
}
