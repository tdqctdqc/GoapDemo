using Godot;
using System;
using System.Linq;

public class CoverGoal : GoapGoal<Division>
{
    private Army _enemy; 
    public CoverGoal(Army enemy)
    {
        _enemy = enemy;
    }
    public override GoapState<Division> GetInitialState(GoapAgent<Division> agent)
    {
        var coverPos = _enemy.Position + _enemy.Facing.Normalized() * 10f;
        var inCoverPos = DivisionAgent.PositionVar.Branch(coverPos);
        var attacking = DivisionAgent.AttackingVar.Branch(true);
        TargetStates.Add(new GoapState<Division>(inCoverPos, attacking));

        Actions = agent.Actions.ToList();
        var moveToCoverPos = new MoveAction(coverPos, 1f);
        var attack = new AttackAction(_enemy);
        Actions.Add(moveToCoverPos);
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
