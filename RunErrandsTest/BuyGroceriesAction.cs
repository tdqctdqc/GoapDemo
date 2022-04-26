using Godot;
using System;

public class BuyGroceriesAction : GoapAction<Errander>
{
    private RunErrandsGoal _runErrandsGoal;
    private GoapVar<Vector2, Errander> _currentPos => RunErrandsGoal.CurrentPosition;
    private GoapVar<Vector2, Errander> _groceryStorePos => _runErrandsGoal.GroceryStorePosition;
    private GoapVar<bool, Errander> _groceriesUnbought => RunErrandsGoal.GroceriesUnbought;
    public BuyGroceriesAction(RunErrandsGoal goal) : 
        base("BuyGroceries", a => { SetInstanceMembers(a, goal);})
    {
    }

    private static void SetInstanceMembers(GoapAction<Errander> action, RunErrandsGoal goal)
    {
        var thisAction = (BuyGroceriesAction) action;
        thisAction._runErrandsGoal = goal;
    }

    public override GoapGoal<Errander> GetSuccessorGoal(GoapActionArgs args)
    {
        return null;
    }

    public override float Cost(GoapState<Errander> state)
    {
        var currentPos = state.GetFluent<Vector2>(RunErrandsGoal.CurrentPosition.Name).Value;
        var groceryPos = state.GetFluent<Vector2>(_groceryStorePos.Name).Value;
        return currentPos.DistanceTo(groceryPos);
    }

    public override string Descr(GoapActionArgs args)
    {
        return "Buying groceries";
    }

    public override GoapState<Errander> TransformContextForSuccessorGoal(GoapState<Errander> actionContext)
    {
        return null;
    }

    public override GoapActionArgs ApplyToState(GoapState<Errander> state)
    {
        var groceryPos = state.GetFluent<Vector2>(_groceryStorePos.Name).Value;
        state.MutateFluent(_groceriesUnbought, false);
        state.MutateFluent(_currentPos, groceryPos);
        return new GoapActionArgs();
    }
}
