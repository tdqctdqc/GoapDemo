using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class GoapState<TAgent> : IGoapState
{
    protected Dictionary<string, IGoapAgentVarInstance<TAgent>> _varNameDic;
    
    public GoapState(params IGoapAgentVarInstance<TAgent>[] stateVars)
    {
        _varNameDic = new Dictionary<string, IGoapAgentVarInstance<TAgent>>();
        for (int i = 0; i < stateVars.Length; i++)
        {
            var stateVar = stateVars[i];
            _varNameDic.Add(stateVar.Name, stateVar);
        }
    }
    private GoapVarInstance<TValue, TAgent> GetVarTyped<TValue>(string name) where TValue : struct
    {
        if (_varNameDic.ContainsKey(name))
        {
            if (_varNameDic[name] is GoapVarInstance<TValue, TAgent> v)
            {
                return v;
            }
        }
        return null;
    }

    private IGoapAgentVarInstance<TAgent> GetVar(string name)
    {
        if (_varNameDic.ContainsKey(name))
        {
            return _varNameDic[name];
        }
        return null;
    }
    public bool CheckVarMatch<TValue>(string name, TValue value)
    {
        var goapVar = GetVar(name);
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

    public IGoapAgentVarInstance<TAgent> GetVarTypeChecked(string name, Type type)
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
            var candVar = candidateState.GetVarTypeChecked(variable.Name, variable.ValueType);
            if (typedVar.SatisfiedBy(candidateState) == false) return false;
        }
        return true; 
    }

    public float GetHeuristicDistance(GoapState<TAgent> state)
    {
        var cost = 0f;
        foreach (var entry in _varNameDic)
        {
            var variable = entry.Value;
            var typedVar = GetVarTypeChecked(variable.Name, variable.ValueType);
            var candVar = state.GetVarTypeChecked(variable.Name, variable.ValueType);
            cost += typedVar.GetHeuristicCost(candVar);
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
