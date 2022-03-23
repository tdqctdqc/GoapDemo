using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class GoapState<TAgent> : IGoapState
{
    protected IReadOnlyList<IGoapVar> _vars => _varsPrivate;
    private List<IGoapVar> _varsPrivate;
    public GoapState(params IGoapVar[] stateVars)
    {
        _varsPrivate = new List<IGoapVar>(stateVars);
    }
    public GoapVar<TValue, TAgent> GetVar<TValue>(GoapVar<TValue, TAgent> match)
    {
        var goapVar = _vars.Where(v => v.Name == match.Name).FirstOrDefault();
        if (goapVar != null)
        {
            if (goapVar is GoapVar<TValue, TAgent> v)
            {
                return v;
            }
        }
        return null;
    }

    public void MutateVar<TValue>(GoapVar<TValue, TAgent> varToMutate, TValue newValue)
    {
        var newVar = varToMutate.Branch(newValue);
        _varsPrivate.Remove(varToMutate);
        _varsPrivate.Add(newVar);
    }

    private IGoapVar GetVarTypeChecked(string name, Type type)
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

    public IGoapVar GetGenericVar(IGoapVar match)
    {
        return GetVarTypeChecked(match.Name, match.ValueType);
    }
}
