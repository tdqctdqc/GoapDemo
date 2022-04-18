using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class GoapGoalNode<TAgent>
{
    public GoapGoal<TAgent> Goal { get; private set; }
    public List<GoapSubGoalNode<TAgent>> SubGoalNodes { get; private set; }
    public List<GoapAgent<TAgent>> Agents { get; private set; }
    public List<GoapPlanNode<TAgent>> PlanNodes { get; private set; }

    public GoapGoalNode(GoapGoal<TAgent> goal, List<GoapAgent<TAgent>> agents)
    {
        Goal = goal;
        Agents = agents;
        PlanNodes = new List<GoapPlanNode<TAgent>>();
        SubGoalNodes = new List<GoapSubGoalNode<TAgent>>();
    }
    public void DoPlanning()
    {
        SetupSubGoalNodes();
        DistributeAgentsToSubGoals();
        SetupPlanNodes();

        for (int i = 0; i < PlanNodes.Count; i++)
        {
            PlanNodes[i].DoPlanning();
        }
    }

    public void AccumulateLeaves(List<GoapActionNode<TAgent>> list)
    {
        for (int i = 0; i < PlanNodes.Count; i++)
        {
            PlanNodes[i].AccumulateLeaves(list);
        }
    }

    private void SetupSubGoalNodes()
    {
        for (int i = 0; i < Goal.SubGoals.Count; i++)
        {
            var subGoalNode = new GoapSubGoalNode<TAgent>(Goal.SubGoals[i]);
            SubGoalNodes.Add(subGoalNode);
        }
    }

    private void DistributeAgentsToSubGoals()
    {
        var agents = Agents.ToList();
        //temporarily just assigning naively
        // if (agents.Count < SubGoalNodes.Count) throw new Exception("not enough agents");
        //
        // int iter = 0;
        // while (agents.Count > 0)
        // {
        //     iter = iter % SubGoalNodes.Count;
        //     SubGoalNodes[iter].AddAgent(agents[0]);
        //     iter++;
        //     agents.RemoveAt(0);
        // }

        foreach (var subGoalNode in SubGoalNodes)
        {
            subGoalNode.AddAgent(agents[0]);
        }
    }
    private void SetupPlanNodes()
    {
        for (int i = 0; i < SubGoalNodes.Count; i++)
        {
            var subGoalNode = SubGoalNodes[i];
            var planNode = new GoapPlanNode<TAgent>(subGoalNode.SubGoal, subGoalNode.Agents);
            PlanNodes.Add(planNode);
        }
    }
}
