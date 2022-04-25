using System.Collections.Generic;

namespace GoapDemo.BreakfastTest
{
    public class EatToastAction : GoapAction<Eater>
    {
        [ExplicitVar] private static GoapVar<bool, Eater> _hungry => ConsumeBreakfastGoal.Hungry;
        [TestCase] private static IGoapAction GetTestCase() => new EatToastAction();
        public EatToastAction() : base("EatToast", a => { })
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
            return "Eating toast";
        }
        public override GoapActionArgs ApplyToState(GoapState<Eater> state)
        {
            state.MutateFluent(_hungry, false);
            return new GoapActionArgs();
        }
    }
}
