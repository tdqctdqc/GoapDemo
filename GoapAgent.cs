using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract class GoapAgent<TAgent> : IGoapAgent
{
    public TAgent Entity { get; private set; }
    public Type AgentType => Entity.GetType();
    public List<GoapAction<TAgent>> Actions { get; private set; }
    public List<IGoapAgentVar<TAgent>> Vars { get; private set; }
    public GoapAgent(TAgent entity)
    {
        Actions = new List<GoapAction<TAgent>>();
        Vars = new List<IGoapAgentVar<TAgent>>();
        Entity = entity; 
    }
    public virtual GoapSchedule<TAgent> GetSchedule(List<GoapGoal<TAgent>> goals)
    {
        goals = goals.OrderBy(g => g.Priority(this)).ToList();
        var plans = goals.Select(g => GoapPlanner.PlanOld(this, g, 100)).ToList();
        
        var schedule = new GoapSchedule<TAgent>(typeof(TAgent));
        for (int i = 0; i < plans.Count; i++)
        {
            schedule.AddEntry(goals[i], plans[i], new List<GoapAgent<TAgent>>{this});
        }
        return schedule;
    }


    public IGoapVarInstance[] GetBranchedVars()
    {
        return Vars.Select(v => v.BranchGeneric(this)).ToArray();
    }

    public object GetAgent()
    {
        return Entity; 
    }
}
