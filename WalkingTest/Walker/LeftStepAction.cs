using Godot;
using System;

public class LeftStepAction : GoapAction<Walker>
{
    public LeftStepAction() : base("LeftStepAction")
    {
    }
    public override bool Valid(GoapState<Walker> state)
    {
        var leftForward = state.GetVar(WalkerAgent.LeftFootForward);
        return leftForward.Value == false; 
    }
    public override float Cost(GoapState<Walker> state)
    {
        var leftForward = state.GetVar(WalkerAgent.LeftFootForward);
        return leftForward.Value == false ? 1f : Mathf.Inf;
    }

    public override string Descr(GoapActionArgs args)
    {
        var from = args.GetArg<Vector2>("from");
        var to = args.GetArg<Vector2>("to");
        return $"Stepping with left foot from {from} to {to}";
    }
    public override GoapActionArgs ApplyToState(GoapState<Walker> state)
    {
        var leftFootForwardVar = state.GetVar(WalkerAgent.LeftFootForward);
        var homePos = state.GetVar(WalkerAgent.HomeLocationVar).Value;
        var strideLength = state.GetVar(WalkerAgent.StrideLength).Value;
        var posVar = state.GetVar(WalkerAgent.PositionVar);
        var pos = posVar.Value;
        
        var dist = pos.DistanceTo(homePos);
        var distTravel = Mathf.Min(dist, strideLength);
        
        var newPos = pos + (homePos - pos).Normalized() * distTravel;
        DoStrideEffect(state, newPos, posVar);
        SwitchLeftFootForwardEffect(state, leftFootForwardVar);
        
        var args = new GoapActionArgs();
        args.AddArg("from", pos);
        args.AddArg("to", newPos);
        return args; 
    }

    private void DoStrideEffect(GoapState<Walker> state, Vector2 newPos, GoapVar<Vector2, Walker> posVar)
    {
        state.MutateVar(posVar, newPos);
    }
    private void SwitchLeftFootForwardEffect(GoapState<Walker> state, GoapVar<bool, Walker> leftFootForwardVar)
    {
        state.MutateVar(leftFootForwardVar, true);
    }
}
