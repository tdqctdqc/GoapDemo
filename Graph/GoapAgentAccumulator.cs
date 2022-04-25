using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class GoapAgentAccumulator<TAgent> 
{
    public List<GoapAgent<TAgent>> AccumulatedAgents { get; private set; }
    public float DifficultyUnsatisfied { get; private set; }
    private List<float> _needMagnitudes;
    private List<Func<GoapAgent<TAgent>, float>> _agentCapabilityFuncs;

    private GoapSubGoalNode<TAgent> _node;
    public GoapAgentAccumulator(GoapSubGoalNode<TAgent> node, GoapState<TAgent> initialState)
    {
        _node = node;
        AccumulatedAgents = new List<GoapAgent<TAgent>>();
        _needMagnitudes = new List<float>();
        _agentCapabilityFuncs = new List<Func<GoapAgent<TAgent>, float>>();
        float diffUnsatisfied = 0f;
        foreach (var agentReq in _node.SubGoal.AgentRequirements)
        {
            float needMagnitude = agentReq.NeedMagnitudeFunc(initialState);
            diffUnsatisfied += needMagnitude;
            _needMagnitudes.Add(needMagnitude);
            _agentCapabilityFuncs.Add(agentReq.AgentCapabilityFunc);
        }

        DifficultyUnsatisfied = diffUnsatisfied;
    }
    public virtual void AccumulateAgents(List<GoapAgent<TAgent>> availableAgents, 
        int numToTake)
    {
        IEnumerable<GoapAgent<TAgent>> ordered =  availableAgents
            .OrderBy(a => GetAgentSuitability(a));
        while (numToTake > 0 && availableAgents.Count > 0)
        {
            var agentToTake = ordered.ElementAt(0);
            ordered = ordered.Skip(1);
            availableAgents.Remove(agentToTake);
            AddAgent(agentToTake);
            numToTake--;
        }
    }
    public void AddAgent(GoapAgent<TAgent> agent)
    {
        AccumulatedAgents.Add(agent);
        float newUnsatisfied = 0f;
        for (int i = 0; i < _needMagnitudes.Count; i++)
        {
            var needCapability = _agentCapabilityFuncs[i](agent);
            _needMagnitudes[i] = Mathf.Max(0f, _needMagnitudes[i] - needCapability);
            newUnsatisfied += _needMagnitudes[i];
        }    
        
        DifficultyUnsatisfied = newUnsatisfied;
    }

    private float GetAgentSuitability(GoapAgent<TAgent> agent)
    {
        float suitability = 0f;
        for (int i = 0; i < _needMagnitudes.Count; i++)
        {
            float need = _needMagnitudes[i];
            if (need == 0f) continue;
            float benefit = Mathf.Min(1f, _agentCapabilityFuncs[i](agent) / need);
            suitability += benefit;
        }

        return suitability;
    }
}
