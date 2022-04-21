using System.Collections.Generic;
using System.Linq;

namespace GoapDemo.BreakfastTest
{
    public class ConsumeBreakfastGoal : GoapGoal<Eater>
    {
        [ExplicitVar] private static GoapVar<bool, Eater> _hungry 
            = BoolVar<Eater>.Construct("Hungry", 1f, e => e.Hungry);
        [ExplicitVar] private static GoapVar<bool, Eater> _caffeinated 
            = BoolVar<Eater>.Construct("Caffeinated", 1f, e => e.Caffeinated);

        [SubGoal] private static GoapSubGoal<Eater> _subGoal
            = new ConsumeBreakfastSubGoal(1f);
        public ConsumeBreakfastGoal() : base()
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

        private class ConsumeBreakfastSubGoal : GoapSubGoal<Eater>
        {
            public override List<GoapAction<Eater>> Actions => _actions;
            protected override void BuildActions()
            {
                _actions = new List<GoapAction<Eater>>()
                {
                    new EatToastAction(),
                    new DrinkCoffeeAction()
                };
            }

            public override float GetAgentCapability(GoapAgent<Eater> agent)
            {
                return 1f;
            }

            private static List<GoapAction<Eater>> _actions;
            public ConsumeBreakfastSubGoal(float difficulty) : base(GetTargetState(), difficulty)
            {
            }
            private static GoapState<Eater> GetTargetState()
            {
                var targetState = new GoapState<Eater>
                (
                    new GoapFluent<bool, Eater>(_hungry, false),
                    new GoapFluent<bool, Eater>(_caffeinated, true)
                );
                return targetState;
            }
        }
    }
}


