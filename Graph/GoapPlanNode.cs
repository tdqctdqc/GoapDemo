using Godot;
using System;
using System.Collections.Generic;

public class GoapPlanNode<TAgent>
{
    public GoapPlan<TAgent> Plan { get; private set; }
    public GoapSubGoal<TAgent> SubGoal { get; private set; }
    public List<GoapAgent<TAgent>> Agents { get; private set; }
    public List<GoapActionNode<TAgent>> ActionNodes { get; private set; }
    public List<GoapGoalNode<TAgent>> Children { get; private set; }

    public GoapPlanNode(GoapSubGoal<TAgent> subGoal, List<GoapAgent<TAgent>> agents)
    {
        SubGoal = subGoal;
        Agents = new List<GoapAgent<TAgent>>(agents);
        ActionNodes = new List<GoapActionNode<TAgent>>();
        Children = new List<GoapGoalNode<TAgent>>();
    }

    public void DoPlanning(GoapState<TAgent> initialState)
    {
        Plan = GoapPlanner.PlanSubGoal(SubGoal, initialState, 100);
        

        if (Plan == null)
        {
            GD.Print("failed plan");
            return;
        }
        for (int i = 0; i < Plan.Actions.Count; i++)
        {
            var action = Plan.Actions[i];
            var args = Plan.ActionArgs[i];
            var actionNode = new GoapActionNode<TAgent>(action, this, args);
            ActionNodes.Add(actionNode);
            if (action.GetSuccessorGoal(args) is GoapGoal<TAgent> g)
            {
                var context = actionNode.GetContext();
                var goalInitialState = action.TransformContextForSuccessorGoal(context);
                var goalNode = new GoapGoalNode<TAgent>(g, Agents, goalInitialState);
                Children.Add(goalNode);
            }
            else
            {
                Children.Add(null);
            }
        }

        for (int i = 0; i < Children.Count; i++)
        {
            var child = Children[i];
            if (child != null)
            {
                child.DoPlanning();
            }
        }
    }
    public void AccumulateLeaves(List<GoapActionNode<TAgent>> list)
    {
        if (Plan == null) return;
        for (int i = 0; i < Plan.Actions.Count; i++)
        {
            var actionNode = ActionNodes[i];
            var assocGoal = Children[i];
            if (assocGoal != null)
            {
                assocGoal.AccumulateLeaves(list);
            }
            else
            {
                list.Add(actionNode);
            }
        }
    }
}
