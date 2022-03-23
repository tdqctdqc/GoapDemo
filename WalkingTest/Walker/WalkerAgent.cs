using Godot;
using System;

public class WalkerAgent : GoapAgent<Walker>
{
    public static GoapVar<Vector2, Walker> PositionVar =
        new Vec2Var<Walker>("Position", 1f, w => w.Location, Vector2.Zero);
    public static GoapVar<Vector2, Walker> HomeLocationVar =
        new Vec2Var<Walker>("HomeLocation", 1f, w => w.HomeLocation, Vector2.One * 100f);
    public static GoapVar<float, Walker> StrideLength =
        new FloatVar<Walker>("StrideLength", 1f, w => w.StrideLength, 10f);
    public static GoapVar<bool, Walker> LeftFootForward =
        new BoolVar<Walker>("LeftFootForward", w => w.LeftFootForward, true);

    public static LeftStepAction LeftStepAction = new LeftStepAction();
    public static RightStepAction RightStepAction = new RightStepAction();
    public WalkerAgent(Walker agent) : base(agent)
    {
        BuildActions();
    }

    protected override void BuildActions()
    {
        Actions.Add(LeftStepAction);
        Actions.Add(RightStepAction);
    }
}
