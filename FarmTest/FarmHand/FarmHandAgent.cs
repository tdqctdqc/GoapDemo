using Godot;
using System;

public class FarmHandAgent : GoapAgent<FarmHand> 
{
    public FarmHandAgent(FarmHand agent) : base(agent)
    {
        BuildActions();
    }

    protected override void BuildActions()
    {
        
    }
}
