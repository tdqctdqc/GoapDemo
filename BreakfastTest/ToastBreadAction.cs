using Godot;
using System;
using System.Collections.Generic;

public class ToastBreadAction : GoapAction<Eater>
{
    public static GoapVar<bool, Eater> BreadIsToasted =
        BoolVar<Eater>.Construct("BreadIsToasted", 1f, e => e.Bread.Toasted);
    public ToastBreadAction() : base("PutBreadInToaster")
    {
    }

    protected override void SetupVars()
    {
        Vars = new List<IGoapAgentVar<Eater>>
        {
            BreadIsToasted
        };
    }

    public override GoapState<Eater> TransformContextForSuccessorGoal(GoapState<Eater> actionContext)
    {
        return null;
    }

    public override GoapGoal<Eater> GetSuccessorGoal(GoapActionArgs args)
    {
        return null;
    }
    public override bool Valid(GoapState<Eater> state)
    {
        return true; 
    }
    public override float Cost(GoapState<Eater> state)
    {
        return 1f;
    }
    public override string Descr(GoapActionArgs args)
    {
        return "Toasting bread";
    }
    public override GoapActionArgs ApplyToState(GoapState<Eater> state)
    {
        state.MutateVar(BreadIsToasted, true);
        return new GoapActionArgs();
    }
}
