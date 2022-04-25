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
    private GoapState<TAgent> _initialState;
    public GoapGoalNode(GoapGoal<TAgent> goal, List<GoapAgent<TAgent>> agents)
    {
        Goal = goal;
        Agents = agents;
        PlanNodes = new List<GoapPlanNode<TAgent>>();
        SubGoalNodes = new List<GoapSubGoalNode<TAgent>>();
        _initialState = goal.GetInitialState(agents);
    }
    public GoapGoalNode(GoapGoal<TAgent> goal, List<GoapAgent<TAgent>> agents,
        GoapState<TAgent> initialState)
    {
        Goal = goal;
        Agents = agents;
        PlanNodes = new List<GoapPlanNode<TAgent>>();
        SubGoalNodes = new List<GoapSubGoalNode<TAgent>>();
        _initialState = initialState;
    }
    public void DoPlanning()
    {
        SetupSubGoalNodes();
        SetupPlanNodes();

        for (int i = 0; i < PlanNodes.Count; i++)
        {
            PlanNodes[i].DoPlanning(_initialState);
        }
    }

    public void AccumulateLeaves(List<GoapActionNode<TAgent>> list)
    {
        for (int i = 0; i < PlanNodes.Count; i++)
        {
            PlanNodes[i].AccumulateLeaves(list);
        }
    }

    private bool EnoughAgentsToDoInParallel(List<GoapAgent<TAgent>> agents)
    {
        return agents.Count >= Goal.SubGoals.Count;
    }
    private void SetupSubGoalNodes()
    {
        if (Goal.SubGoals.Count == 1)
        {
            SetupSingleSubGoal();
        }
        else if (EnoughAgentsToDoInParallel(Agents))
        {
            SetupSubGoalNodesParallel();
        }
        else
        {
            SetupUnionSubGoal();
        }
    }

    private void SetupSubGoalNodesParallel()
    {
        for (int i = 0; i < Goal.SubGoals.Count; i++)
        {
            var subGoalNode = new GoapSubGoalNode<TAgent>(Goal.SubGoals[i], _initialState);
            SubGoalNodes.Add(subGoalNode);
        }
        DistributeAgentsToSubGoals();
    }

    private void SetupSingleSubGoal()
    {
        var subGoalNode = new GoapSubGoalNode<TAgent>(Goal.SubGoals[0], _initialState);
        foreach (var agent in Agents)
        {
            subGoalNode.Accumulator.AddAgent(agent);
        }
        SubGoalNodes.Add(subGoalNode);
    }
    private void SetupUnionSubGoal()
    {
        var unionSubGoal = GoapSubGoal<TAgent>.GetUnionSubGoal(Goal.SubGoals.ToArray());
        var unionSubGoalNode = new GoapSubGoalNode<TAgent>(unionSubGoal, _initialState);
        foreach (var agent in Agents)
        {
            unionSubGoalNode.Accumulator.AddAgent(agent);
        }
        SubGoalNodes.Add(unionSubGoalNode);
    }

    private void DistributeAgentsToSubGoals()
    {
        var agents = Agents.ToList();
        
        for (int i = 0; i < SubGoalNodes.Count; i++)
        {
            SubGoalNodes[i].AccumulateAgents(agents, 1);
        }
        
        IEnumerable<GoapSubGoalNode<TAgent>> subGoalNodesRanked = SubGoalNodes.OrderByDescending(s => s.Accumulator.DifficultyUnsatisfied);

        while (agents.Count > 0)
        {
            var topSubGoal = subGoalNodesRanked.First();
            topSubGoal.AccumulateAgents(agents, 1);
            subGoalNodesRanked = subGoalNodesRanked.OrderByDescending(s => s.Accumulator.DifficultyUnsatisfied);
        }
    }
    private void SetupPlanNodes()
    {
        for (int i = 0; i < SubGoalNodes.Count; i++)
        {
            var subGoalNode = SubGoalNodes[i];
            var planNode = new GoapPlanNode<TAgent>(subGoalNode.SubGoal, subGoalNode.Accumulator.AccumulatedAgents);
            PlanNodes.Add(planNode);
        }
    }
}
