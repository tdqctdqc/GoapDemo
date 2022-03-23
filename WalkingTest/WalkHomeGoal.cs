using Godot;
using System;

public class WalkHomeGoal : GoapGoal<Walker>
{
    public override GoapState<Walker> GetInitialState(GoapAgent<Walker> agent)
    {
        var targetPosVar = WalkerAgent.PositionVar.Branch(agent.Agent.HomeLocation);
        TargetState = new GoapState<Walker>(targetPosVar);
        
        var leftForward = WalkerAgent.LeftFootForward.Branch(agent);
        var strideLength = WalkerAgent.StrideLength.Branch(agent);
        var pos = WalkerAgent.PositionVar.Branch(agent);
        var homePos = WalkerAgent.HomeLocationVar.Branch(agent);
        
        var initState = new GoapState<Walker>(leftForward, strideLength, pos, homePos);
        return initState; 
    }

    public override float Priority(GoapAgent<Walker> agent)
    {
        return 1f;
    }

    public override float SubordinateCapability(IGoapAgent agent)
    {
        return 1f;
    }
}
