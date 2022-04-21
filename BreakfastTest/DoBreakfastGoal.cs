using System.Collections.Generic;
using System.Linq;

namespace GoapDemo.BreakfastTest
{
public class DoBreakfastGoal : GoapGoal<Eater>
{
    [ExplicitVar] private static GoapVar<bool, Eater> _breakfastIsMade =
        BoolVar<Eater>.Construct("BreakfastIsMade", 1f, e => e.Bread.Buttered && e.Bread.Toasted && e.Coffee.Made);
    [ExplicitVar] private static GoapVar<bool, Eater> _hasConsumedBreakfast =
        BoolVar<Eater>.Construct("HasEatenBreakfast", 1f, e => e.Hungry == false);

    [SubGoal] private static GoapSubGoal<Eater> _subGoal =
        new DoBreakfastSubGoal(1f);
    public DoBreakfastGoal() : base()
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

    private class DoBreakfastSubGoal : GoapSubGoal<Eater>
    {
        public override List<GoapAction<Eater>> Actions => _actions;
        private static List<GoapAction<Eater>> _actions;

        protected override void BuildActions()
        {
            _actions = new List<GoapAction<Eater>>()
            {
                new MakeBreakfastAction(),
                new ConsumeBreakfastAction()
            };
        }

        public override float GetAgentCapability(GoapAgent<Eater> agent)
        {
            return 1f;
        }

        public DoBreakfastSubGoal(float diff) : base(GetTargetState(), diff)
        {
        
        }
        private static GoapState<Eater> GetTargetState()
        {
            var targetState = new GoapState<Eater>
            (
                new GoapFluent<bool, Eater>(_hasConsumedBreakfast, true)
            );
            return targetState;
        }
    }
}
}

