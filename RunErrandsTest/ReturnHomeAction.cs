using Godot;
using System;

public class ReturnHomeAction : GoapAction<Errander>
{
    private GoapVar<Vector2, Errander> _currentPos => RunErrandsGoal.CurrentPosition;
    private GoapVar<Vector2, Errander> _homePos => RunErrandsGoal.HomePosition;
    
    public ReturnHomeAction() 
        : base("ReturnHome", a => { })
    {
    }

    public override GoapGoal<Errander> GetSuccessorGoal(GoapActionArgs args)
    {
        return null;
    }

    public override float Cost(GoapState<Errander> state)
    {
        var homePos = state.GetFluent<Vector2>(_homePos.Name).Value;
        var currentPos = state.GetFluent<Vector2>(_currentPos.Name).Value;
        return currentPos.DistanceTo(homePos);
    }

    public override string Descr(GoapActionArgs args)
    {
        return "Returning home";
    }

    public override GoapState<Errander> TransformContextForSuccessorGoal(GoapState<Errander> actionContext)
    {
        return null;
    }

    public override GoapActionArgs ApplyToState(GoapState<Errander> state)
    {
        var homePos = state.GetFluent<Vector2>(_homePos.Name).Value;
        state.MutateFluent(_currentPos, homePos);
        return new GoapActionArgs();
    }
}
