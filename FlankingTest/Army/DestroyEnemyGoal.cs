// using Godot;
// using System;
// using System.Linq;
//
// public class DestroyEnemyGoal : GoapGoal<Army>
// {
//     // public override GoapState<Army> GetInitialState(GoapAgent<Army> agent)
//     // {
//     //     var enemyEngagedVar = ArmyAgent.EnemyIsEngaged.Branch(true);
//     //     var enemyFlankedVar = ArmyAgent.EnemyIsFlanked.Branch(true);
//     //     AddSubGoal(new GoapState<Army>(enemyEngagedVar, enemyFlankedVar));
//     //
//     //     Actions = agent.Actions.ToList();
//     //     var flankAction = new FlankAction(agent.Entity.Enemy);
//     //     var coverAction = new CoverAction(agent.Entity.Enemy);
//     //     Actions.Add(flankAction);
//     //     Actions.Add(coverAction);
//     //     return new GoapState<Army>(agent.GetBranchedVars());
//     // }
//
//     public override float Priority(GoapAgent<Army> agent)
//     {
//         return 1f; 
//     }
// }
