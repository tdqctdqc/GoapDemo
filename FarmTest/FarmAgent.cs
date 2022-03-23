using Godot;
using System;
using System.Collections.Generic;

public class FarmAgent : GoapAgent<Farm> 
{
    public static GoapVar<float, Farm> FieldCutPercent = new FloatVar<Farm>("fieldCutPercent", 1f, f => 0f, 0f);
    public static CutFieldAction CutFieldAction = new CutFieldAction();

    public FarmAgent(Farm agent) : base(agent)
    {
        BuildActions();
    }
    
    protected override void BuildActions()
    {
        Actions.Add(CutFieldAction);
    }
}


public class CutFieldAction : GoapAction<Farm>
{
    public CutFieldAction() : base("cutField")
    {
    }
     
    public override bool Valid(GoapState<Farm> state)
    {
        return state.GetVar<float>(FarmAgent.FieldCutPercent) != null;
    }
     
    public override float Cost(GoapState<Farm> state)
    {
        var fieldCutPercent = state.GetVar<float>(FarmAgent.FieldCutPercent).Value;
        return (1f - fieldCutPercent);
    }
     
    public override string Descr(GoapActionArgs args)
    {
        return "Cutting field";
    }

    public override GoapActionArgs ApplyToState(GoapState<Farm> state)
    {
        var fieldCutPercent = state.GetVar<float>(FarmAgent.FieldCutPercent);
        state.MutateVar<float>(fieldCutPercent, 1f);
        return new GoapActionArgs();
    }
}