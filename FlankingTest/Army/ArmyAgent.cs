using Godot;
using System;

public class ArmyAgent : GoapAgent<Army>
{
    public static GoapVar<Vector2, Army> PositionVar
        = Vec2Var<Army>.ConstructScaledHeuristic("Position", 1f, a => a.Position);
    public static GoapVar<Vector2, Army> FacingVar
        = Vec2Var<Army>.ConstructScaledHeuristic("Facing", 1f, a => a.Facing);

    public static GoapVar<bool, Army> EnemyIsEngaged
        = BoolVar<Army>.Construct("EnemyEngaged", 100f, a => a.Enemy.IsEngaged);
    public static GoapVar<bool, Army> EnemyIsFlanked
        = BoolVar<Army>.Construct("EnemyFlanked", 100f, a => a.Enemy.IsFlanked);
    public static GoapAction<Army> FlankAction
        = new FlankAction();
    public static GoapAction<Army> CoverAction
        = new CoverAction();
    public ArmyAgent(Army agent) : base(agent)
    {
        Vars.Add(PositionVar);
        Vars.Add(FacingVar);
        Vars.Add(EnemyIsEngaged);
        Vars.Add(EnemyIsFlanked);
        Actions.Add(FlankAction);
        Actions.Add(CoverAction);
    }
}
