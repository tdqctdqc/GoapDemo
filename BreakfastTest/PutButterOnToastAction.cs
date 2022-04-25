using System;
using System.Collections.Generic;

namespace GoapDemo.BreakfastTest
{
    public class PutButterOnToastAction : GoapAction<Eater>
    {
        [ExplicitVar] private static GoapVar<bool, Eater> _breadIsToastedVar => MakeBreakfastGoal.BreadIsToasted;
        [ExplicitVar] private static GoapVar<bool, Eater> _breadIsButtered => MakeBreakfastGoal.BreadIsButtered;
        [Requirement] private static Func<GoapState<Eater>, bool> _breadToastedReq 
            = s => s.CheckVarMatch<bool>(_breadIsToastedVar.Name, true);
        [TestCase] private static IGoapAction GetTestCase() => new PutButterOnToastAction();
        public PutButterOnToastAction() : base("PutButterOnToast", a => { })
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
            state.MutateFluent(_breadIsButtered, true);
            return new GoapActionArgs();
        }
    }
}
