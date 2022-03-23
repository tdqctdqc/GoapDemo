using Godot;
using System;
using System.Collections.Generic;

public class GoapSchedule<TAgent> 
{
    public List<GoapScheduleEntry<TAgent>> Entries { get; private set; }
    public Type ActingAgentType { get; private set; } 
    public GoapSchedule(Type actingAgentType)
    {
        ActingAgentType = actingAgentType;
        Entries = new List<GoapScheduleEntry<TAgent>>();
    }
    public void AddEntry<T>(GoapGoal<TAgent> goal, GoapPlan<TAgent> plan, List<GoapAgent<T>> agents)
    {
        if (ActingAgentType.IsAssignableFrom(typeof(T)) == false)
        {
            throw new ArgumentException("trying to add entry with wrong acting agent type");
        }

        var list = new List<IGoapAgent>(agents);
        var entry = new GoapScheduleEntry<TAgent>(goal, plan, list);
        Entries.Add(entry);
    }

    public void MergeSchedule(GoapSchedule<TAgent> otherSchedule)
    {
        if (otherSchedule.ActingAgentType != ActingAgentType)
        {
            throw new ArgumentException("trying to merge schedules for diff acting agent types");
        }
        Entries.AddRange(otherSchedule.Entries);
    }
}
public class GoapScheduleEntry<TAgent>
{
    public GoapGoal<TAgent> Goal { get; private set; }
    public GoapPlan<TAgent> Plan { get; private set; }
    public List<IGoapAgent> Agents { get; private set; }

    public GoapScheduleEntry(GoapGoal<TAgent> goal, GoapPlan<TAgent> plan, List<IGoapAgent> agents)
    {
        Goal = goal;
        Plan = plan;
        Agents = agents;
    }
}
