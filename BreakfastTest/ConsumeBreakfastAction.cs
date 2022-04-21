using System;
using System.Collections.Generic;

namespace GoapDemo.BreakfastTest
{
    public class ConsumeBreakfastAction : GoapAction<Eater> 
    {
        [ExplicitVar] private static GoapVar<bool, Eater> _breakfastIsMade =
            BoolVar<Eater>.Construct("BreakfastIsMade", 1f, e => e.Bread.Buttered && e.Bread.Toasted && e.Coffee.Made);
        [ExplicitVar] private static GoapVar<bool, Eater> _hasConsumedBreakfast =
            BoolVar<Eater>.Construct("HasEatenBreakfast", 1f, e => e.Hungry == false);
        [SuccessorVar] private static GoapVar<bool, Eater> _hungry =
            BoolVar<Eater>.Construct("Hungry", 1f, e => e.Hungry);
        [SuccessorVar] private static GoapVar<bool, Eater> _caffeinated =
            BoolVar<Eater>.Construct("Caffeinated", 1f, e => e.Caffeinated);
        public ConsumeBreakfastAction() : base("ConsumeBreakfast")
        {
        }

        public override GoapState<Eater> TransformContextForSuccessorGoal(GoapState<Eater> actionContext)
        {
            var consumedVar = actionContext.GetVar<bool>(_hasConsumedBreakfast.Name);
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
            return state.CheckVarMatch(_breakfastIsMade.Name, true);
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
            state.MutateVar(_hasConsumedBreakfast, true);
            return new GoapActionArgs();
        }
    }
}
