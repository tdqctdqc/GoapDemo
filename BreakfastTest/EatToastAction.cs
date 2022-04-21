using System.Collections.Generic;

namespace GoapDemo.BreakfastTest
{
    public class EatToastAction : GoapAction<Eater>
    {
        public static GoapVar<bool, Eater> _hungry =
            BoolVar<Eater>.Construct("Hungry", 1f, e => e.Hungry);
        public EatToastAction() : base("EatToast")
        {
        }

        protected override void SetupVars()
        {
            ExplicitVars = new List<IGoapAgentVar<Eater>>
            {
                _hungry
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
            return true;
        }
        public override float Cost(GoapState<Eater> state)
        {
            return 1f;
        }
        public override string Descr(GoapActionArgs args)
        {
            return "Eating toast";
        }
        public override GoapActionArgs ApplyToState(GoapState<Eater> state)
        {
            state.MutateVar(_hungry, false);
            return new GoapActionArgs();
        }
    }
}
