using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public static class GoapPlanner 
{
    public static void PlanGoal<TAgent>(GoapGoal<TAgent> goal, List<GoapAgent<TAgent>> agents)
    {
        var goalNode = new GoapGoalNode<TAgent>(goal, agents);
        goalNode.DoPlanning();
        var actionNodes = new List<GoapActionNode<TAgent>>();
        goalNode.AccumulateLeaves(actionNodes);
        for (int i = 0; i < actionNodes.Count; i++)
        {
            var descr = actionNodes[i].Action.Descr(actionNodes[i].Args);
            GD.Print(descr);
        }
    }

    public static GoapPlan<TAgent> PlanSubGoal<TAgent>(GoapSubGoal<TAgent> subGoal, 
        GoapState<TAgent> startState,
        List<GoapAgent<TAgent>> agents, int maxPlanIter)
    {
        var startPlan = new GoapPlan<TAgent>(startState);
                    
        GoapPlan<TAgent> current; 
            
        Func<GoapState<TAgent>, bool> finished = (s) => subGoal.TargetState.SatisfiedBy(s);
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
            var planNeighbors = subGoal.Actions
                .Where(a => a.Valid(current.EndState))
                .Select(a => current.ExtendPlan(a));
            openPlans.AddRange(planNeighbors);
            if (openPlans.Count == 0) break;
            openPlans = openPlans
                .OrderBy(p => p.Cost + subGoal.TargetState.GetHeuristicDistance(p.EndState))
                .ToList();
        }

        return null;
    }
}
