using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class GoapPlan<TAgent> 
{
    public List<GoapAction<TAgent>> Actions { get; private set; }
    public List<GoapActionArgs> ActionArgs { get; private set; }
    public GoapState<TAgent> EndState { get; private set; }
    public float Cost => _cost;
    private readonly float _cost;
    public float Difficulty => _diff;
    private readonly float _diff;
    
    public GoapPlan()
    {
        Actions = new List<GoapAction<TAgent>>();
        ActionArgs = new List<GoapActionArgs>();
    }
    public GoapPlan(GoapState<TAgent> startState)
    {
        EndState = startState.Clone();
        Actions = new List<GoapAction<TAgent>>();
        ActionArgs = new List<GoapActionArgs>();
    }
    private GoapPlan(GoapState<TAgent> startState, List<GoapAction<TAgent>> actions, 
                    List<GoapActionArgs> args, float costCumul, float diff)
    {
        _cost = costCumul;
        _diff = diff; 
        EndState = startState.Clone();
        Actions = actions;
        ActionArgs = args;
    }
    public GoapPlan<TAgent> ExtendPlan(GoapAction<TAgent> action)
    {
        var actionCost = action.Cost(EndState);
        float diff = Mathf.Max(actionCost, _diff);
        var newActions = Actions.ToList();
        newActions.Add(action);
        
        var newState = EndState.Clone();
        var newArg = action.ApplyToState(newState);
        var extendedArgs = ActionArgs.ToList();
        extendedArgs.Add(newArg);
        return new GoapPlan<TAgent>(newState, newActions, extendedArgs, Cost + actionCost, diff);
    }

    public void Print()
    {
        GD.Print("PLAN ACTIONS");
        for (int i = 0; i < Actions.Count; i++)
        {
            var action = Actions[i];
            GD.Print($"Action {i}: {action.Name}");
            GD.Print(action.Descr(ActionArgs[i]));
        }
        GD.Print("FINAL STATE");
        EndState.PrintState();
    }
}
