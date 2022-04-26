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
        GoapState<TAgent> startState, int maxPlanIter)
    {
        var search = new GoapPlanAStarSearch<TAgent>(subGoal, startState, 100);
        search.DoSearch();
        return search.Success;
    }
}
