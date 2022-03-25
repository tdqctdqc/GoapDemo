using Godot;
using System;

public class DivisionAgent : GoapAgent<Division>
{
    public static GoapVar<Vector2, Division> PositionVar
        = Vec2Var<Division>.ConstructScaledHeuristic("Position", 1f, a => a.Position);
    public static GoapVar<Vector2, Division> FacingVar
        = Vec2Var<Division>.ConstructScaledHeuristic("Facing", 1f, a => a.Facing);
    public static GoapVar<bool, Division> AttackingVar
        = BoolVar<Division>.Construct("Attacking", 1f, a => false);

    public DivisionAgent(Division agent) : base(agent)
    {
        Vars.Add(PositionVar);
        Vars.Add(FacingVar);
        Vars.Add(AttackingVar);

    }
}
