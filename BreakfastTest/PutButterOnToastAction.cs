using System;
using System.Collections.Generic;

namespace GoapDemo.BreakfastTest
{
    public class PutButterOnToastAction : GoapAction<Eater>
    {
        [ExplicitVar] private static GoapVar<bool, Eater> _breadIsToasted =
            BoolVar<Eater>.Construct("BreadIsToasted", 1f, e => e.Bread.Toasted);
        [ExplicitVar] private static GoapVar<bool, Eater> _breadIsButtered =
            BoolVar<Eater>.Construct("BreadIsButtered", 1f, e => e.Bread.Buttered);
        
        [Requirement] private static Func<GoapState<Eater>, bool> _breadToastedReq 
            = s => s.CheckVarMatch<bool>(_breadIsToasted.Name, true);
        public PutButterOnToastAction() : base("PutButterOnToast")
        {
        }

        public override GoapState<Eater> TransformContextForSuccessorGoal(GoapState<Eater> actionContext)
        {
            return null;
        }

        public override GoapGoal<Eater> GetSuccessorGoal(GoapActionArgs args)
        {
            return null;
        }
        public override float Cost(GoapState<Eater> state)
        {
            return 1f;
        }
        public override string Descr(GoapActionArgs args)
        {
            return "Putting butter on toast";
        }
        public override GoapActionArgs ApplyToState(GoapState<Eater> state)
        {
            state.MutateVar(_breadIsButtered, true);
            return new GoapActionArgs();
        }
    }
}
