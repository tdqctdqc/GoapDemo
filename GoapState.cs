using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class GoapState<TAgent> : IGoapState
{
    protected IReadOnlyList<IGoapVarInstance> _vars => _varsPrivate;
    private List<IGoapVarInstance> _varsPrivate;
    public GoapState(params IGoapVarInstance[] stateVars)
    {
        _varsPrivate = new List<IGoapVarInstance>(stateVars);
    }

    public GoapVarInstance<TValue, TAgent> GetVar<TValue>(IGoapVar match) where TValue : struct
    {
        var goapVar = _vars.Where(v => v.Name == match.Name).FirstOrDefault();
        if (goapVar != null)
        {
            if (goapVar is GoapVarInstance<TValue, TAgent> v)
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
        var varToMutateInstance = GetVar<TValue>(varToMutate);
        _varsPrivate.Remove(varToMutateInstance);
        _varsPrivate.Add(newVarInstance);
    }

    public IGoapVarInstance GetVarTypeChecked(string name, Type type)
    {
        var goapVar = _vars.Where(v => v.Name == name)
                        .Where(v => v.ValueType == type)    
                        .FirstOrDefault();
        return goapVar;
    }
    
    public bool SatisfiedBy(GoapState<TAgent> candidateState)
    {
        foreach (var v in _vars)
        {
            var typedVar = GetVarTypeChecked(v.Name, v.ValueType);
            var candVar = candidateState.GetVarTypeChecked(v.Name, v.ValueType);
            if (typedVar.SatisfiedBy(candidateState) == false) return false;
        }
        return true; 
    }

    public float GetHeuristicDistance(GoapState<TAgent> state)
    {
        var cost = 0f;
        foreach (var v in _vars)
        {
            var typedVar = GetVarTypeChecked(v.Name, v.ValueType);
            var candVar = state.GetVarTypeChecked(v.Name, v.ValueType);
            cost += typedVar.GetHeuristicCost(candVar);
        }
        return cost; 
    }
    public GoapState<TAgent> Clone()
    {
        return new GoapState<TAgent>(_vars.ToArray());
    }

    public void PrintState()
    {
        for (int i = 0; i < _vars.Count; i++)
        {
            var vari = _vars[i];
            GD.Print(vari.Name + " " + vari.GetValue());
        }
    }

}
