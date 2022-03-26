using Godot;
using System;

public class WalkerAgent : GoapAgent<Walker>
{
    public static GoapVar<Vector2, Walker> PositionVar =
        Vec2Var<Walker>.ConstructScaledHeuristic("Position", 1f, w => w.Location);

    public static GoapVar<Vector2, Walker> HomeLocationVar =
        Vec2Var<Walker>.ConstructScaledHeuristic("HomeLocation", 1f, w => w.HomeLocation);

    public static GoapVar<float, Walker> StrideLength =
        FloatVar<Walker>.ConstructScaleHeuristic("StrideLength", 1f, w => w.StrideLength);

    public static GoapVar<bool, Walker> LeftFootForward =
        BoolVar<Walker>.Construct("LeftFootForward", 100f, w => w.LeftFootForward);

    public static LeftStepAction LeftStepAction = new LeftStepAction();
    public static RightStepAction RightStepAction = new RightStepAction();
    public WalkerAgent(Walker entity) : base(entity)
    {
        Actions.Add(LeftStepAction);
        Actions.Add(RightStepAction);
        Vars.Add(PositionVar);
        Vars.Add(HomeLocationVar);
        Vars.Add(StrideLength);
        Vars.Add(LeftFootForward);
    }
}
