using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class GoapState<TAgent> : IGoapState
{
    protected Dictionary<string, IGoapAgentFluent<TAgent>> _varNameDic;
    
    public GoapState(params IGoapAgentFluent<TAgent>[] stateVars)
    {
        _varNameDic = new Dictionary<string, IGoapAgentFluent<TAgent>>();
        for (int i = 0; i < stateVars.Length; i++)
        {
            var stateVar = stateVars[i];
            _varNameDic.Add(stateVar.Name, stateVar);
        }
    }

    
    public bool CheckVarMatch<TValue>(string name, TValue value)
    {
        var goapVar = GetVarAgnostic(name);
        return goapVar != null ? goapVar.GetValue().Equals(value) : false;
    }

    public GoapState<TAgent> ExtendState(GoapAction<TAgent> action, out GoapActionArgs args)
    {
        var newState = Clone();
        args = action.ApplyToState(newState);
        return newState;
    }
    public void MutateVar<TValue>(GoapVar<TValue, TAgent> varToMutate, TValue newValue) where TValue : struct
    {
        var newVarInstance = varToMutate.Branch(newValue);
        if(_varNameDic.ContainsKey(varToMutate.Name))
            _varNameDic.Remove(varToMutate.Name);
        _varNameDic.Add(newVarInstance.Name, newVarInstance);
    }

    public GoapFluent<TValue, TAgent> GetVar<TValue>(string name) where TValue : struct
    {
        if (GetVarTypeChecked(name, typeof(TValue)) is GoapFluent<TValue, TAgent> v)
        {
            return v;
        }

        return null;
    }
    private IGoapAgentFluent<TAgent> GetVarAgnostic(string name)
    {
        if (_varNameDic.ContainsKey(name))
        {
            return _varNameDic[name];
        }
        return null;
    }
    private IGoapAgentFluent<TAgent> GetVarTypeChecked(string name, Type type)
    {
        if (_varNameDic.ContainsKey(name))
        {
            if (_varNameDic[name].ValueType == type)
            {
                return _varNameDic[name];
            }
        }
        return null;
    }
    
    public bool SatisfiedBy(GoapState<TAgent> candidateState)
    {
        foreach (var entry in _varNameDic)
        {
            var variable = entry.Value;
            var typedVar = GetVarTypeChecked(variable.Name, variable.ValueType);
            if (typedVar.SatisfiedBy(candidateState) == false) return false;
        }
        return true; 
    }

    public float GetHeuristicDistance(GoapState<TAgent> candState)
    {
        var cost = 0f;
        foreach (var entry in _varNameDic)
        {
            var variable = entry.Value;
            var candVar = candState.GetVarTypeChecked(variable.Name, variable.ValueType);
            
            
            var marginalCost = candVar != null 
                                ? variable.GetHeuristicCost(candVar.GetValue())
                                : Mathf.Inf;
            cost += marginalCost;
        }
        return cost; 
    }
    public GoapState<TAgent> Clone()
    {
        return new GoapState<TAgent>(_varNameDic.Values.ToArray());
    }

    public void PrintState()
    {
        foreach (var entry in _varNameDic)
        {
            var variable = entry.Value;
            GD.Print(variable.Name + " " + variable.GetValue());
        }
    }

}
