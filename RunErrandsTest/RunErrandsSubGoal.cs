using Godot;
using System;

public class RunErrandsSubGoal : GoapSubGoal<Errander>
{
    [TargetFluent] private static GoapFluent<bool, Errander> _returnBooksTarget
        = new (RunErrandsGoal.BooksUnreturned, false);
    [TargetFluent] private static GoapFluent<bool, Errander> _cashChecksTarget
        = new (RunErrandsGoal.ChecksUncashed, false);
    [TargetFluent] private static GoapFluent<bool, Errander> _buyGroceriesTarget
        = new (RunErrandsGoal.GroceriesUnbought, false);
    [TargetFluent] private static GoapFluent<bool, Errander> _returnHomeTarget
        = new(RunErrandsGoal.AtHome, true);
        
    [AvailableAction] private GoapAction<Errander> _returnBooksAction;
    [AvailableAction] private GoapAction<Errander> _cashChecksAction;
    [AvailableAction] private GoapAction<Errander> _buyGroceriesAction;
    [AvailableAction] private GoapAction<Errander> _returnHomeAction;
    protected RunErrandsSubGoal(RunErrandsGoal goal) 
        : base(sg => { SetInstanceMembers(sg, goal); })
    {
    }

    private static void SetInstanceMembers(GoapSubGoal<Errander> subGoal,
        RunErrandsGoal goal)
    {
        var runErrandsSubGoal = (RunErrandsSubGoal) subGoal;
        runErrandsSubGoal._buyGroceriesAction = new BuyGroceriesAction(goal);
        runErrandsSubGoal._cashChecksAction = new CashCheckAction(goal);
        runErrandsSubGoal._returnBooksAction = new ReturnBooksAction(goal);
        runErrandsSubGoal._returnHomeAction = new ReturnHomeAction();
    }
}
