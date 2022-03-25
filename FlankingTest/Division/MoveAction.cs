using Godot;
using System;

public class MoveAction : GoapAction<Division>
{
    private Vector2 _dest;
    private float _moveCost;
    public MoveAction(Vector2 dest, float moveCost) : base("Move")
    {
        _dest = dest;
        _moveCost = moveCost;
    }

    public override GoapGoal<TSubAgent> GetAssocGoal<TSubAgent>(GoapActionArgs args)
    {
        return null;
    }

    public override bool Valid(GoapState<Division> state)
    {
        return true;
    }

    public override float Cost(GoapState<Division> state)
    {
        var pos = state.GetVar<Vector2>(DivisionAgent.PositionVar).Value;
        return pos.DistanceTo(_dest) * _moveCost;
    }

    public override string Descr(GoapActionArgs args)
    {
        return $"Moving to {args.GetArg<Vector2>("Destination").Value}";
    }

    public override GoapActionArgs ApplyToState(GoapState<Division> state)
    {
        state.MutateVar(DivisionAgent.PositionVar, _dest);
        var args = new GoapActionArgs();
        args.AddArg("Destination", _dest);
        return args; 
    }
}
