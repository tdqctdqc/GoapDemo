using System.Collections.Generic;
using System;

namespace GoapDemo.BreakfastTest
{
    public class ToastBreadAction : GoapAction<Eater>
    {
        [ExplicitVar] private static GoapVar<bool, Eater> _breadIsToasted =
            BoolVar<Eater>.ConstructEqualityHeuristic("BreadIsToasted", 1f, e => e.Bread.Toasted);

        public ToastBreadAction() : base("PutBreadInToaster", a => { })
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
            return "Toasting bread";
        }
        public override GoapActionArgs ApplyToState(GoapState<Eater> state)
        {
            state.MutateFluent(_breadIsToasted, true);
            return new GoapActionArgs();
        }
    }
}
