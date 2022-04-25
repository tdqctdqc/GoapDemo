using Godot;
using System;
using System.Collections.Generic;

public class GoapSubGoalNode<TAgent>
{
    public GoapSubGoal<TAgent> SubGoal { get; private set; }
    public GoapAgentAccumulator<TAgent> Accumulator { get; private set; }
    
    public GoapSubGoalNode(GoapSubGoal<TAgent> subGoal, GoapState<TAgent> initialState)
    {
        SubGoal = subGoal;
        Accumulator = new GoapAgentAccumulator<TAgent>(this, initialState);
    }

    public void AccumulateAgents(List<GoapAgent<TAgent>> availableAgents, int numToTake)
    {
        Accumulator.AccumulateAgents(availableAgents, numToTake);
    }
}
