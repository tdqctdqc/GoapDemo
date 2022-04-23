using System.Collections.Generic;
using System.Linq;

namespace GoapDemo.BreakfastTest
{
    public class MakeBreakfastGoal : GoapGoal<Eater>
    {
        [ExplicitVar] private static GoapVar<bool, Eater> _breadIsToasted 
            = BoolVar<Eater>.ConstructEqualityHeuristic("BreadIsToasted", 1f, e => e.Bread.Toasted);
        [ExplicitVar] private static GoapVar<bool, Eater> _breadIsButtered 
            = BoolVar<Eater>.ConstructEqualityHeuristic("BreadIsButtered", 1f, e => e.Bread.Buttered);
        [ExplicitVar] private static GoapVar<bool, Eater> _coffeeIsMade 
            = BoolVar<Eater>.ConstructEqualityHeuristic("CoffeeIsMade", 1f, e => e.Coffee.Made);

        [SubGoal] private static GoapSubGoal<Eater> _makeToastSubGoal
            = new MakeToastSubGoal(1f);
        [SubGoal] private static GoapSubGoal<Eater> _makeCoffeeSubGoal
            = new MakeCoffeeSubGoal(1f);
        public MakeBreakfastGoal() : base()
        {
        
        }
        public override float Priority(GoapAgent<Eater> agent)
        {
            return 1f;
        }

        public override GoapState<Eater> GetInitialState(List<GoapAgent<Eater>> agents)
        {
            return GetInitialStateFirstAgentMethod(agents);
        }

        private class MakeToastSubGoal : GoapSubGoal<Eater>
        {
            [AvailableAction] private static GoapAction<Eater> _toastBreadAction
                = new ToastBreadAction();

            [AvailableAction] private static GoapAction<Eater> _butterToastAction
                = new PutButterOnToastAction();

            public override float GetAgentCapability(GoapAgent<Eater> agent)
            {
                return 1f;
            }

            public MakeToastSubGoal(float difficulty) : base(GetTargetState(), difficulty)
            {
            }
            private static GoapState<Eater> GetTargetState()
            {
                var targetState = new GoapState<Eater>
                (
                    new GoapFluent<bool, Eater>(_breadIsToasted, true),
                    new GoapFluent<bool, Eater>(_breadIsButtered, true)
                );
                return targetState;
            }
        }
        private class MakeCoffeeSubGoal : GoapSubGoal<Eater>
        {
            [AvailableAction] private static GoapAction<Eater> _makeCoffeeAction
                = new MakeCoffeeAction();

            public override float GetAgentCapability(GoapAgent<Eater> agent)
            {
                return 1f;
            }

            public MakeCoffeeSubGoal(float difficulty) : base(GetTargetState(), difficulty)
            {
            }
            private static GoapState<Eater> GetTargetState()
            {
                var targetState = new GoapState<Eater>
                (
                    new GoapFluent<bool, Eater>(_coffeeIsMade, true)
                );
                return targetState;
            }
        }
    }
}













