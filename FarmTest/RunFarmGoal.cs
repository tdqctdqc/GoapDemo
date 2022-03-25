using Godot;
using System;

public class RunFarmGoal : GoapGoal<Farm>
{
    public RunFarmGoal()
    {
        var fieldCutPercentVar = FarmAgent.FieldCutPercent.Branch(1f);
        TargetStates.Add(new GoapState<Farm>(fieldCutPercentVar));
    }
    public override GoapState<Farm> GetInitialState(GoapAgent<Farm> agent)
    {
        var fieldCutPercentVar = FarmAgent.FieldCutPercent.Branch(0f);
        var initState = new GoapState<Farm>(fieldCutPercentVar);
        return initState; 
    }
    
    public override float Priority(GoapAgent<Farm> agent)
    {
        return 1f;
    }

    public override float SubordinateCapability(IGoapAgent agent)
    {
        return 1f;
    }
}
