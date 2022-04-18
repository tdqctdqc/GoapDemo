using Godot;
using System;
using System.Collections.Generic;

public class EaterAgent : GoapAgent<Eater>
{
    public static GoapVar<bool, Eater> BreakfastMadeVar
        = BoolVar<Eater>.Construct("BreakfastMade", 1f, e => false);
    public static GoapVar<bool, Eater> HungryVar
        = BoolVar<Eater>.Construct("Hungry", 1f, e => e.Hungry);
    
    public EaterAgent(Eater entity) : base(entity)
    {
        Vars = new List<IGoapAgentVar<Eater>>()
        {
            BreakfastMadeVar,
            HungryVar
        };
    }
}
