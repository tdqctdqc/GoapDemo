using Godot;
using System;

namespace GoapDemo.BreakfastTest
{
    public class MakeCoffeeSubGoal : GoapSubGoal<Eater>
    {
        [AgentRequirement] private static GoapAgentRequirement<Eater> _req
            = new GoapAgentRequirement<Eater>(s => 1f, a => 1f);
        [TargetFluent] private static GoapFluent<bool, Eater> _coffeeIsMadeFluent
            = new GoapFluent<bool, Eater>(MakeBreakfastGoal.CoffeeIsMade, true);
        [AvailableAction] private static GoapAction<Eater> _makeCoffeeAction
                    = new MakeCoffeeAction();
        public MakeCoffeeSubGoal() 
            : base(sg => { })
        {
        }
    }
}
