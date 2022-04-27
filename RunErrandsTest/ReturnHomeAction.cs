using Godot;
using System;

public class ReturnHomeAction : GoapAction<Errander>
{
    private static GoapVar<Vector2, Errander> _currentPos => RunErrandsGoal.CurrentPosition;
    private static GoapVar<Vector2, Errander> _homePos => RunErrandsGoal.HomePosition;

    [Requirement] private static Func<GoapState<Errander>, bool> _req
        => s =>
        {
            // s.CheckVarMatch(RunErrandsGoal.AtHome.Name, false);

            if (s.CheckVarMatch(RunErrandsGoal.GroceriesUnbought.Name, true))
                return false;
            if (s.CheckVarMatch(RunErrandsGoal.ChecksUncashed.Name, true))
                return false;
            if (s.CheckVarMatch(RunErrandsGoal.BooksUnreturned.Name, true))
                return false;
            return true;
            // var homePos = s.GetFluent<Vector2>(_homePos.Name).Value;
            // var currentPos = s.GetFluent<Vector2>(_currentPos.Name).Value;
            // return homePos != currentPos;
        };
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
        return currentPos.DistanceTo(homePos) * 3f;
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
