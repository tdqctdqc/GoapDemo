using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class GoapState<TAgent> : IGoapState
{
    protected Dictionary<string, IGoapAgentFluent<TAgent>> _fluentNameDic;
    
    public GoapState(params IGoapAgentFluent<TAgent>[] stateVars)
    {
        _fluentNameDic = new Dictionary<string, IGoapAgentFluent<TAgent>>();
        for (int i = 0; i < stateVars.Length; i++)
        {
            var stateVar = stateVars[i];
            _fluentNameDic.Add(stateVar.Name, stateVar);
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
    public void MutateFluent<TValue>(GoapVar<TValue, TAgent> varToMutate, TValue newValue) where TValue : struct
    {
        var newVarInstance = varToMutate.Branch(newValue);
        if(_fluentNameDic.ContainsKey(varToMutate.Name))
            _fluentNameDic.Remove(varToMutate.Name);
        _fluentNameDic.Add(newVarInstance.Name, newVarInstance);
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
        if (_fluentNameDic.ContainsKey(name))
        {
            return _fluentNameDic[name];
        }
        return null;
    }
    private IGoapAgentFluent<TAgent> GetVarTypeChecked(string name, Type type)
    {
        if (_fluentNameDic.ContainsKey(name))
        {
            if (_fluentNameDic[name].ValueType == type)
            {
                return _fluentNameDic[name];
            }
        }
        return null;
    }
    
    public bool SatisfiedBy(GoapState<TAgent> candidateState)
    {
        foreach (var entry in _fluentNameDic)
        {
            var fluent = entry.Value;
            if (fluent.SatisfiedBy(candidateState) == false) return false;
        }
        return true; 
    }

    public float GetHeuristicDistance(GoapState<TAgent> candState)
    {
        var cost = 0f;
        foreach (var entry in _fluentNameDic)
        {
            var fluent = entry.Value;
            var marginalCost = fluent.GetHeuristicCost(candState);
            cost += marginalCost;
        }
        return cost; 
    }
    public GoapState<TAgent> Clone()
    {
        return new GoapState<TAgent>(_fluentNameDic.Values.ToArray());
    }

    public void PrintState()
    {
        foreach (var entry in _fluentNameDic)
        {
            var variable = entry.Value;
            GD.Print(variable.Name + " " + variable.GetValue());
        }
    }
}
