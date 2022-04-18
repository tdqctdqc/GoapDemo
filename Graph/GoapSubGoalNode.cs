using Godot;
using System;
using System.Collections.Generic;

public class GoapSubGoalNode<TAgent>
{
    public GoapSubGoal<TAgent> SubGoal { get; private set; }
    public List<GoapAgent<TAgent>> Agents { get; private set; }
    public float DifficultyUnsatisfied { get; private set; }
    
    public GoapSubGoalNode(GoapSubGoal<TAgent> subGoal)
    {
        SubGoal = subGoal;
        DifficultyUnsatisfied = subGoal.Difficulty;
        Agents = new List<GoapAgent<TAgent>>();
    }

    public void AddAgent(GoapAgent<TAgent> agent)
    {
        Agents.Add(agent);
        var capability = SubGoal.SubordinateCapability(agent);
        DifficultyUnsatisfied = Mathf.Max(0f, DifficultyUnsatisfied - capability);
    }
}
