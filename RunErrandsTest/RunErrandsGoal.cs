using Godot;
using System;
using System.Collections.Generic;

public class RunErrandsGoal : GoapGoal<Errander>
{
    [ExplicitVar] public GoapVar<Vector2, Errander> GroceryStorePosition { get; private set; }
    [ExplicitVar] public GoapVar<Vector2, Errander> LibraryPosition { get; private set; }
    [ExplicitVar] public GoapVar<Vector2, Errander> BankPosition { get; private set; }

    [ExplicitVar] public static GoapVar<Vector2, Errander> HomePosition => _homePosLazy.Value;
    private static Lazy<GoapVar<Vector2, Errander>> _homePosLazy = new(
        () => Vec2Var<Errander>.ConstructDistanceHeuristic("HomePosition", 1f, e => e.HomePosition));
    
    [ExplicitVar] public static GoapVar<Vector2, Errander> CurrentPosition => _currentPosLazy.Value;
    private static Lazy<GoapVar<Vector2, Errander>> _currentPosLazy = new(
        () => Vec2Var<Errander>.ConstructDistanceHeuristic("CurrentPosition", 1f, e => e.CurrentPosition));
    
    [ExplicitVar] public static GoapVar<bool, Errander> BooksUnreturned
        = BoolVar<Errander>.ConstructEqualityHeuristic("BooksUnreturned", 10f, e => e.NeedToReturnBooks);
    [ExplicitVar] public static GoapVar<bool, Errander> ChecksUncashed
        = BoolVar<Errander>.ConstructEqualityHeuristic("ChecksUncashed", 10f, e => e.NeedToCashCheck);
    [ExplicitVar] public static GoapVar<bool, Errander> GroceriesUnbought
        = BoolVar<Errander>.ConstructEqualityHeuristic("GroceriesUnbought", 10f, e => e.NeedToBuyGroceries);
    

    [ImplicitVar] public static GoapVar<bool, Errander> AtHome
        = BoolVar<Errander>.ConstructImplicitDistanceHeuristic<Vector2>("AtHome",
            e => e.CurrentPosition == e.HomePosition,
            HomePosition, (p, q) => p.DistanceTo(q) * 0f, CurrentPosition);


    [SubGoal] public GoapSubGoal<Errander> _subGoal;
    public RunErrandsGoal(Vector2 groceryStoreLoc, Vector2 libLoc, Vector2 bankLoc) 
        : base("RunErrands", g => { SetupInstanceVars(g, groceryStoreLoc, libLoc, bankLoc); })
    {
    }

    private static void SetupInstanceVars(GoapGoal<Errander> goal, Vector2 groceryStoreLoc, Vector2 libraryLoc, Vector2 bankLoc)
    {
        var runErrandsGoal = (RunErrandsGoal) goal;
        runErrandsGoal.GroceryStorePosition
            = Vec2Var<Errander>.ConstructDistanceHeuristic("GroceryStorePosition", 1f, e => groceryStoreLoc);
        runErrandsGoal.LibraryPosition
            = Vec2Var<Errander>.ConstructDistanceHeuristic("LibraryPosition", 1f, e => libraryLoc);
        runErrandsGoal.BankPosition
            = Vec2Var<Errander>.ConstructDistanceHeuristic("BankPosition", 1f, e => bankLoc);
        runErrandsGoal._subGoal = new RunErrandsSubGoal(runErrandsGoal);
    }

    public override float Priority(GoapAgent<Errander> agent)
    {
        return 1f;
    }

    public override GoapState<Errander> GetInitialState(List<GoapAgent<Errander>> agents)
    {
        return GetInitialStateFirstAgentMethod(agents);
    }
}
