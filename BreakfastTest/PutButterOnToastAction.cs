using System.Collections.Generic;

namespace GoapDemo.BreakfastTest
{
    public class PutButterOnToastAction : GoapAction<Eater>
    {
        public static GoapVar<bool, Eater> BreadIsToasted =
            BoolVar<Eater>.Construct("BreadIsToasted", 1f, e => e.Bread.Toasted);
        public static GoapVar<bool, Eater> BreadIsButtered =
            BoolVar<Eater>.Construct("BreadIsButtered", 1f, e => e.Bread.Buttered);
        public PutButterOnToastAction() : base("PutButterOnToast")
        {
        }

        protected override void SetupVars()
        {
            ExplicitVars = new List<IGoapAgentVar<Eater>>
            {
                BreadIsButtered,
                BreadIsToasted
            };
        }

        public override GoapState<Eater> TransformContextForSuccessorGoal(GoapState<Eater> actionContext)
        {
            return null;
        }

        public override GoapGoal<Eater> GetSuccessorGoal(GoapActionArgs args)
        {
            return null;
        }
        public override bool Valid(GoapState<Eater> state)
        {
            return state.CheckVarMatch<bool>(BreadIsToasted.Name, true);
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
            state.MutateVar(BreadIsButtered, true);
            return new GoapActionArgs();
        }
    }
}
