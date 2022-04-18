using Godot;
using System;
using System.Linq;

// public class WalkHomeGoal : GoapGoal<Walker>
// {
//     // public override GoapState<Walker> GetInitialState(GoapAgent<Walker> agent)
//     // {
//     //     var targetPosVar = WalkerAgent.PositionVar.Branch(agent.Entity.HomeLocation);
//     //     var targetState = new GoapState<Walker>(targetPosVar);
//     //     AddSubGoal(targetState);
//     //
//     //     Actions = agent.Actions.ToList();
//     //     var initState = new GoapState<Walker>(agent.GetBranchedVars());
//     //     return initState; 
//     // }
//     public override float Priority(GoapAgent<Walker> agent)
//     {
//         return 1f;
//     }
// }
