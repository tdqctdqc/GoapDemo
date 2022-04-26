using Godot;
using System;

public class CashCheckAction : GoapAction<Errander>
{
    private RunErrandsGoal _runErrandsGoal;
    private GoapVar<Vector2, Errander> _bankPos => _runErrandsGoal.BankPosition;
    private static GoapVar<Vector2, Errander> _currentPos => RunErrandsGoal.CurrentPosition;
    private static GoapVar<bool, Errander> _checksUncashed => RunErrandsGoal.ChecksUncashed;
    [Requirement] private static Func<GoapState<Errander>, bool> _req
        => s => s.CheckVarMatch(_checksUncashed.Name, true);
    public CashCheckAction(RunErrandsGoal goal) 
        : base("CashCheck", a => { SetInstanceMembers((CashCheckAction)a, goal);})
    {
    }

    private static void SetInstanceMembers(CashCheckAction thisAction, RunErrandsGoal goal)
    {
        thisAction._runErrandsGoal = goal;
    }

    public override GoapGoal<Errander> GetSuccessorGoal(GoapActionArgs args)
    {
        return null;
    }

    public override float Cost(GoapState<Errander> state)
    {
        var bankPos = state.GetFluent<Vector2>(_bankPos.Name).Value;
        var currentPos = state.GetFluent<Vector2>(_currentPos.Name).Value;
        return bankPos.DistanceTo(currentPos);
    }

    public override string Descr(GoapActionArgs args)
    {
        return "Cashing check";
    }

    public override GoapState<Errander> TransformContextForSuccessorGoal(GoapState<Errander> actionContext)
    {
        return null;
    }

    public override GoapActionArgs ApplyToState(GoapState<Errander> state)
    {
        var bankPos = state.GetFluent<Vector2>(_bankPos.Name).Value;
        state.MutateFluent(_currentPos, bankPos);
        state.MutateFluent(_checksUncashed, false);
        return new GoapActionArgs();
    }
}
