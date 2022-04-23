using System;
using System.Collections.Generic;

namespace GoapDemo.BreakfastTest
{
    public class ConsumeBreakfastAction : GoapAction<Eater> 
    {
        [ExplicitVar] private static GoapVar<bool, Eater> _breakfastIsMade 
            = BoolVar<Eater>.ConstructEqualityHeuristic("BreakfastIsMade", 1f, e => e.Bread.Buttered && e.Bread.Toasted && e.Coffee.Made);
        [ExplicitVar] private static GoapVar<bool, Eater> _hasConsumedBreakfast 
            = BoolVar<Eater>.ConstructEqualityHeuristic("HasEatenBreakfast", 1f, e => e.Hungry == false);
        [SuccessorVar] private static GoapVar<bool, Eater> _hungry 
            = BoolVar<Eater>.ConstructEqualityHeuristic("Hungry", 1f, e => e.Hungry);
        [SuccessorVar] private static GoapVar<bool, Eater> _caffeinated 
            = BoolVar<Eater>.ConstructEqualityHeuristic("Caffeinated", 1f, e => e.Caffeinated);
        
        [Requirement] private static Func<GoapState<Eater>, bool> _req
            = s => s.CheckVarMatch(_breakfastIsMade.Name, true);
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
