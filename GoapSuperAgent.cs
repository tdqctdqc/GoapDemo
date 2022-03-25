using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract class GoapSuperAgent<TAgent, TSubAgent> : GoapAgent<TAgent>
{
    public List<GoapAgent<TSubAgent>> Subordinates { get; private set; }

    public GoapSuperAgent(TAgent agent) : base(agent)
    {
        Subordinates = new List<GoapAgent<TSubAgent>>();
    }

    public void AddSubordinates(params GoapAgent<TSubAgent>[] subs)
    {
        Subordinates.AddRange(subs);
    }
    private GoapSchedule<TAgent> GetSelfSchedule(List<GoapGoal<TAgent>> goals)
    {
        goals = goals.OrderBy(g => g.Priority(this)).ToList();
        var plans = goals.Select(g => GoapPlanner.PlanOld(this, g, 100)).ToList();
        var agentsDistribution = GetSubordinateDistribution(goals, plans)
                                .ToArray();
                                    
        var schedule = new GoapSchedule<TAgent>(typeof(TSubAgent));
        for (int i = 0; i < plans.Count; i++)
        {
            schedule.AddEntry(goals[i], plans[i], agentsDistribution[i]);
        }
        return schedule;
    }

    private GoapSchedule<TSubAgent> GetSubordinateSchedule(List<GoapGoal<TAgent>> goals)
    {
        var selfSchedule = GetSchedule(goals);
        var agentsDistribution = GetSubordinateDistribution(selfSchedule.Entries.Select(e => e.Goal).ToList(), selfSchedule.Entries.Select(e => e.Plan).ToList());
        
        var subSchedule = new GoapSchedule<TSubAgent>(typeof(TSubAgent));
        for (int i = 0; i < selfSchedule.Entries.Count; i++)
        {
            var e = selfSchedule.Entries[i];
            var plan = e.Plan;
            var subGoals = Enumerable.Range(0, e.Plan.Actions.Count)
                .Select(j =>
                    plan.Actions[j].GetAssocGoal<TSubAgent>(plan.ActionArgs[j]))
                .ToList();
            var subSchedules = agentsDistribution[i].Select(a => a.GetSchedule(subGoals)).ToList();
            subSchedules.ForEach(s => subSchedule.MergeSchedule(s));
        }
        
        return subSchedule; 
    }

    private List<GoapAgent<TSubAgent>>[] GetSubordinateDistribution(List<GoapGoal<TAgent>> goals, List<GoapPlan<TAgent>> plans)
    {
        var availableSubs = Subordinates.ToList();
        var agentsDistribution = new List<GoapAgent<TSubAgent>>[plans.Count];
        for (int i = 0; i < plans.Count; i++)
        {
            var plan = plans[i];
            var goal = goals[i];
            var planCost = plan.Cost;

            agentsDistribution[i] = new List<GoapAgent<TSubAgent>>();
            while (availableSubs.Count > 0 && planCost > 0f)
            {
                var sub = availableSubs[0];
                planCost -= goal.SubordinateCapability(sub);
                agentsDistribution[i].Add(sub);
            }
        }

        return agentsDistribution;
    }
}
