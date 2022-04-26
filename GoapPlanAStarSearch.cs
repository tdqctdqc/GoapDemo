using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class GoapPlanAStarSearch<TAgent> : AStarSearch<GoapPlan<TAgent>>
{
    public GoapPlanAStarSearch(GoapSubGoal<TAgent> subGoal, GoapState<TAgent> startState, int maxIter)
        : base(new GoapPlan<TAgent>(startState), p => GetNeighbors(subGoal, p), GetCost, p => GetHeuristic(subGoal, p), 
            p => GetSuccess(subGoal, p), maxIter)
    {
    }

    private static IEnumerable<GoapPlan<TAgent>> GetNeighbors(GoapSubGoal<TAgent> subGoal, GoapPlan<TAgent> plan)
    {
        return subGoal.Actions
            .Where(a => a.Valid(plan.EndState))
            .Select(a => plan.ExtendPlan(a));
    }

    private static float GetCost(GoapPlan<TAgent> plan)
    {
        return plan.Cost;
    }

    private static float GetHeuristic(GoapSubGoal<TAgent> subGoal, GoapPlan<TAgent> plan)
    {
        return subGoal.TargetState.GetHeuristicDistance(plan.EndState);
    }

    private static bool GetSuccess(GoapSubGoal<TAgent> subGoal, GoapPlan<TAgent> plan)
    {
        return subGoal.TargetState.SatisfiedBy(plan.EndState);
    }
}
