using Godot;
using System;

namespace GoapDemo.BreakfastTest
{
    public class ConsumeBreakfastSubGoal : GoapSubGoal<Eater>
    {
        [TargetFluent] private static GoapFluent<bool, Eater> _hungryFluent 
            = new GoapFluent<bool, Eater>(ConsumeBreakfastGoal.Hungry, false);
        
        [TargetFluent] private static GoapFluent<bool, Eater> _caffeinatedFluent 
            = new GoapFluent<bool, Eater>(ConsumeBreakfastGoal.Caffeinated, true);
        
        [AgentRequirement] private static GoapAgentRequirement<Eater> _req 
            = new GoapAgentRequirement<Eater>(s => 1f, a => 1f);
        
        [AvailableAction] private static GoapAction<Eater> _eatToastAction 
            =  new EatToastAction();
        
        [AvailableAction] private static GoapAction<Eater> _drinkCoffeeAction 
            = new DrinkCoffeeAction();
        public ConsumeBreakfastSubGoal() 
            : base("ConsumeBreakfast", sg => { })
        {
        }
    }
}