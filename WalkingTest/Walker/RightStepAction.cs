using Godot;
using System;
using System.Security.AccessControl;

public class RightStepAction : GoapAction<Walker>
{
    public RightStepAction() : base("RightStepAction")
    {
    }

    public override bool Valid(GoapState<Walker> state)
    {
        var leftForward = state.GetVar<bool>(WalkerAgent.LeftFootForward);
        return leftForward.Value; 
    }

    public override float Cost(GoapState<Walker> state)
    {
        var leftForward = state.GetVar<bool>(WalkerAgent.LeftFootForward);
        return leftForward.Value == true ? 1f : Mathf.Inf;
    }

    public override string Descr(GoapActionArgs args)
    {
        var from = args.GetArg<Vector2>("from");
        var to = args.GetArg<Vector2>("to");
        return $"Stepping with right foot from {from.Value} to {to.Value}";
    }

    public override GoapActionArgs ApplyToState(GoapState<Walker> state)
    {
        var leftFootForwardVar = state.GetVar<bool>(WalkerAgent.LeftFootForward);
        var homePos = state.GetVar<Vector2>(WalkerAgent.HomeLocationVar).Value;
        var strideLength = state.GetVar<float>(WalkerAgent.StrideLength).Value;
        var posVar = state.GetVar<Vector2>(WalkerAgent.PositionVar);
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
    private void DoStrideEffect(GoapState<Walker> state, Vector2 newPos, GoapVarInstance<Vector2, Walker> posVar)
    {
        state.MutateVar(posVar.BaseVar, newPos);
    }

    private void SwitchLeftFootForwardEffect(GoapState<Walker> state, GoapVarInstance<bool, Walker> leftFootForwardVar)
    {
        state.MutateVar(leftFootForwardVar.BaseVar, false);
    }
    public override GoapGoal<Walker> GetSuccessorGoal(GoapActionArgs args)
    {
        return null;
    }
}
