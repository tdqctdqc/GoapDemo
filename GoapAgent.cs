using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract class GoapAgent<TAgent> : IGoapAgent
{
    public TAgent Agent { get; private set; }
    public List<GoapAction<TAgent>> Actions { get; private set; }
    public GoapAgent(TAgent agent)
    {
        Actions = new List<GoapAction<TAgent>>();
        Agent = agent; 
    }

    protected abstract void BuildActions();
    public virtual GoapSchedule<TAgent> GetSchedule(List<GoapGoal<TAgent>> goals)
    {
        goals = goals.OrderBy(g => g.Priority(this)).ToList();
        var plans = goals.Select(g => GoapPlanner.Plan(this, g, 100)).ToList();
        
        var schedule = new GoapSchedule<TAgent>(typeof(TAgent));
        for (int i = 0; i < plans.Count; i++)
        {
            schedule.AddEntry(goals[i], plans[i], new List<GoapAgent<TAgent>>{this});
        }
        return schedule;
    }
    

    
}
