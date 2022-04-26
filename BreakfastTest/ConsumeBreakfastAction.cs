using System;
using System.Collections.Generic;

namespace GoapDemo.BreakfastTest
{
    public class ConsumeBreakfastAction : GoapAction<Eater>
    {

        [ExplicitVar] private static GoapVar<bool, Eater> _hasConsumedBreakfast => DoBreakfastGoal.HasConsumedBreakfast;
        [SuccessorVar] private static GoapVar<bool, Eater> _hungry => ConsumeBreakfastGoal.Hungry;
        [SuccessorVar] private static GoapVar<bool, Eater> _caffeinated => ConsumeBreakfastGoal.Caffeinated;
        
        [Requirement] private static Func<GoapState<Eater>, bool> _breakfastMadeReq 
            = s => s.CheckVarMatch(DoBreakfastGoal.BreakfastIsMade.Name, true);

        [TestCase] private static IGoapAction GetTestCase() => new ConsumeBreakfastAction();
        public ConsumeBreakfastAction() : base("ConsumeBreakfast", SetDependentFields)
        {
        }

        private static void SetDependentFields(GoapAction<Eater> action)
        {
            var consumeAction = (ConsumeBreakfastAction) action;
            // consumeAction._req = s => s.CheckVarMatch(_breakfastIsMade.Name, true);
        }
        public override GoapState<Eater> TransformContextForSuccessorGoal(GoapState<Eater> actionContext)
        {
            var consumedVar = actionContext.GetFluent<bool>(_hasConsumedBreakfast.Name);
            if (consumedVar is GoapFluent<bool, Eater> consumed)
            {
                GoapState<Eater> initState = new GoapState<Eater>
                (
                    new GoapFluent<bool, Eater>(_hungry, consumed.Value == false),
                    new GoapFluent<bool, Eater>(_caffeinated, consumed.Value)
                );
                return initState;
            }
            else
            {
                throw new Exception();
            }
        }

        public override GoapGoal<Eater> GetSuccessorGoal(GoapActionArgs args)
        {
            return new ConsumeBreakfastGoal();
        }
        public override float Cost(GoapState<Eater> state)
        {
            return 1f;
        }
        public override string Descr(GoapActionArgs args)
        {
            return "Consuming breakfast";
        }
        public override GoapActionArgs ApplyToState(GoapState<Eater> state)
        {
            state.MutateFluent(_hasConsumedBreakfast, true);
            return new GoapActionArgs();
        }
    }
}
