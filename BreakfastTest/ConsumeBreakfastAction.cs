using System;
using System.Collections.Generic;

namespace GoapDemo.BreakfastTest
{
    public class ConsumeBreakfastAction : GoapAction<Eater> 
    {
        public static GoapVar<bool, Eater> BreakfastIsMade { get; private set; } =
            BoolVar<Eater>.Construct("BreakfastIsMade", 1f, e => e.Bread.Buttered && e.Bread.Toasted && e.Coffee.Made);
        public static GoapVar<bool, Eater> HasConsumedBreakfast { get; private set; } =
            BoolVar<Eater>.Construct("HasEatenBreakfast", 1f, e => e.Hungry == false);
    
        private static GoapVar<bool, Eater> _hungry { get; set; } =
            BoolVar<Eater>.Construct("Hungry", 1f, e => e.Hungry);
        private static GoapVar<bool, Eater> _caffeinated { get; set; } =
            BoolVar<Eater>.Construct("Caffeinated", 1f, e => e.Caffeinated);
        public ConsumeBreakfastAction() : base("ConsumeBreakfast")
        {
        }

        protected override void SetupVars()
        {
            Vars = new List<IGoapAgentVar<Eater>>
            {
                BreakfastIsMade,
                HasConsumedBreakfast
            };
            SuccessorVars = new List<IGoapAgentVar<Eater>>
            {
                _hungry,
                _caffeinated
            };
        }

        public override GoapState<Eater> TransformContextForSuccessorGoal(GoapState<Eater> actionContext)
        {
            var consumedVar = actionContext.GetVar<bool>(HasConsumedBreakfast.Name);
            if (consumedVar is GoapFluent<bool, Eater> consumed)
            {
                GoapState<Eater> initState;
                if (consumed.Value == true)
                {
                    initState = new GoapState<Eater>
                    (
                        new GoapFluent<bool, Eater>(_hungry, false),
                        new GoapFluent<bool, Eater>(_caffeinated, true)
                    );
                }
                else
                {
                    initState = new GoapState<Eater>
                    (
                        new GoapFluent<bool, Eater>(_hungry, true),
                        new GoapFluent<bool, Eater>(_caffeinated, false)
                    );
                }
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
        public override bool Valid(GoapState<Eater> state)
        {
            return state.CheckVarMatch(BreakfastIsMade.Name, true);
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
            state.MutateVar(HasConsumedBreakfast, true);
            return new GoapActionArgs();
        }
    }
}
