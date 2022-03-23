using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public static class GoapPlanner 
{
    public static GoapPlan<TAgent> Plan<TAgent>(GoapAgent<TAgent> agent, GoapGoal<TAgent> goal, int maxPlanIter)
    {
        if (goal == null) return null;
        
        var startState = goal.GetInitialState(agent);
        var startPlan = new GoapPlan<TAgent>(startState);
        
        GoapPlan<TAgent> current; 
        
        Func<GoapState<TAgent>, bool> finished = (s) => goal.TargetState.SatisfiedBy(s);
        int planIter = 0;
        
        List<GoapPlan<TAgent>> openPlans = new List<GoapPlan<TAgent>>();
        openPlans.Add(startPlan);
        while (openPlans.Count > 0 && planIter < maxPlanIter)
        {
            current = openPlans[0];
            if (finished(current.EndState))
            {
                return current;
            }
            openPlans.Remove(current);
            planIter++;
            var planNeighbors = agent.Actions
                .Where(a => a.Valid(current.EndState))
                .Select(a => current.ExtendPlan(a));
            openPlans.AddRange(planNeighbors);
            if (openPlans.Count == 0) break;
            openPlans = openPlans
                .OrderBy(p => p.Cost + goal.TargetState.GetHeuristicDistance(p.EndState))
                .ToList();
        }

        return null;
    }
}
