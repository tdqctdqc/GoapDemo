using Godot;
using System;

public class ReturnBooksAction : GoapAction<Errander>
{
    private RunErrandsGoal _runErrandsGoal;
    private GoapVar<Vector2, Errander> _libraryPos => _runErrandsGoal.LibraryPosition;
    private GoapVar<Vector2, Errander> _currentPos => RunErrandsGoal.CurrentPosition;
    private GoapVar<bool, Errander> _booksUnreturned => RunErrandsGoal.BooksUnreturned;
    public ReturnBooksAction(RunErrandsGoal goal) 
        : base("ReturnBooks", a => { SetInstanceMembers((ReturnBooksAction)a, goal);})
    {
    }

    private static void SetInstanceMembers(ReturnBooksAction thisAction, RunErrandsGoal goal)
    {
        thisAction._runErrandsGoal = goal;
    }

    public override GoapGoal<Errander> GetSuccessorGoal(GoapActionArgs args)
    {
        return null;
    }

    public override float Cost(GoapState<Errander> state)
    {
        var libraryPos = state.GetFluent<Vector2>(_libraryPos.Name).Value;
        var currentPos = state.GetFluent<Vector2>(_currentPos.Name).Value;
        return libraryPos.DistanceTo(currentPos);
    }

    public override string Descr(GoapActionArgs args)
    {
        return "Returning books";
    }

    public override GoapState<Errander> TransformContextForSuccessorGoal(GoapState<Errander> actionContext)
    {
        return null;
    }

    public override GoapActionArgs ApplyToState(GoapState<Errander> state)
    {
        var libraryPos = state.GetFluent<Vector2>(_libraryPos.Name).Value;
        state.MutateFluent(_currentPos, libraryPos);
        state.MutateFluent(_booksUnreturned, false);
        return new GoapActionArgs();
    }
}
