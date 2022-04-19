using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class GoapState<TAgent> : IGoapState
{
    protected Dictionary<string, IGoapVarInstance> _varNameDic;
    
    public GoapState(params IGoapVarInstance[] stateVars)
    {
        _varNameDic = new Dictionary<string, IGoapVarInstance>();
        for (int i = 0; i < stateVars.Length; i++)
        {
            var stateVar = stateVars[i];
            _varNameDic.Add(stateVar.Name, stateVar);
        }
    }

    public GoapVarInstance<TValue, TAgent> GetVar<TValue>(IGoapVar match) where TValue : struct
    {
        if (_varNameDic.ContainsKey(match.Name))
        {
            if (_varNameDic[match.Name] is GoapVarInstance<TValue, TAgent> v)
            {
                return v;
            }
        }
        return null;
    }

    public bool CheckVarMatch<TValue>(IGoapVar match, TValue value) where TValue : struct
    {
        var goapVar = GetVar<TValue>(match);
        return goapVar != null ? goapVar.Value.Equals(value) : false;
    }

    public void MutateVar<TValue>(GoapVar<TValue, TAgent> varToMutate, TValue newValue) where TValue : struct
    {
        var newVarInstance = varToMutate.Branch(newValue);
        if(_varNameDic.ContainsKey(varToMutate.Name))
            _varNameDic.Remove(varToMutate.Name);
        _varNameDic.Add(newVarInstance.Name, newVarInstance);
    }

    public IGoapVarInstance GetVarTypeChecked(string name, Type type)
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
