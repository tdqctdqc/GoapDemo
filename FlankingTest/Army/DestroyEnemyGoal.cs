using Godot;
using System;

public class DestroyEnemyGoal : GoapGoal<Army>
{
    public override GoapState<Army> GetInitialState(GoapAgent<Army> agent)
    {
        var enemyEngagedVar = ArmyAgent.EnemyIsEngaged.Branch(true);
        var enemyFlankedVar = ArmyAgent.EnemyIsFlanked.Branch(true);
        TargetState = new GoapState<Army>(enemyEngagedVar, enemyFlankedVar);
        var initState = new GoapState<Army>(agent.GetBranchedVars());
        return initState; 
    }

    public override float Priority(GoapAgent<Army> agent)
    {
        return 1f; 
    }

    public override float SubordinateCapability(IGoapAgent agent)
    {
        return 1f; 
    }
}
