using Godot;
using System;
using System.Linq;

public class FlankGoal : GoapGoal<Division>
{
    private Army _enemy; 
    public FlankGoal(Army enemy)
    {
        _enemy = enemy; 
    }
    public override GoapState<Division> GetInitialState(GoapAgent<Division> agent)
    {
        var flankPos = _enemy.Position 
                       + _enemy.Facing.Normalized().Rotated(Mathf.Pi / 2f) * 10f;
        var inFlankPos = DivisionAgent.PositionVar.Branch(flankPos);
        var attacking = DivisionAgent.AttackingVar.Branch(true);
        TargetStates.Add(new GoapState<Division>(inFlankPos, attacking));

        Actions = agent.Actions.ToList();
        var moveToFlankPos = new MoveAction(flankPos, 1f);
        var attack = new AttackAction(_enemy);
        Actions.Add(moveToFlankPos);
        Actions.Add(attack);
        return new GoapState<Division>(agent.GetBranchedVars());
    }

    public override float Priority(GoapAgent<Division> agent)
    {
        return 1f;
    }

    public override float SubordinateCapability(IGoapAgent agent)
    {
        return 1f;
    }
}
