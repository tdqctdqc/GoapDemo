using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract class GoapAgent<TAgent> : IGoapAgent
{
    public TAgent Entity { get; private set; }
    public List<GoapAction<TAgent>> Actions { get; private set; }
    public List<IGoapAgentVar<TAgent>> Vars { get; protected set; }
    public GoapAgent(TAgent entity)
    {
        Actions = new List<GoapAction<TAgent>>();
        Entity = entity; 
    }


    public IGoapVarInstance[] GetBranchedVars()
    {
        return Vars.Select(v => v.BranchAgnostic(this)).ToArray();
    }
}
