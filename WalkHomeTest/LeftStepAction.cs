using System;
using System.Collections.Generic;
using Godot;

namespace GoapDemo.WalkHomeTest
{
    public class LeftStepAction : GoapAction<Walker>
    {
        [ExplicitVar] private static GoapVar<Vector2, Walker> _currentPosition => WalkHomeGoal.CurrentPosition;
        [ExplicitVar] private static GoapVar<Vector2, Walker> _homePosition => WalkHomeGoal.HomePosition;
        [ExplicitVar] private static GoapVar<bool, Walker> _leftFootForward => WalkHomeGoal.LeftFootForward;
        [ExplicitVar] private static GoapVar<float, Walker> _strideLength => WalkHomeGoal.StrideLength;
        
        [Requirement] private static Func<GoapState<Walker>, bool> _leftFootBackFunc 
            = s => s.CheckVarMatch(_leftFootForward.Name, false);
        [TestCase] private static IGoapAction GetTestCase() => new LeftStepAction();
        public LeftStepAction() : base("LeftStep", a => { })
        {
        }

        public override GoapState<Walker> TransformContextForSuccessorGoal(GoapState<Walker> actionContext)
        {
            return null;
        }
        public override GoapGoal<Walker> GetSuccessorGoal(GoapActionArgs args)
        {
            return null;
        }
        public override float Cost(GoapState<Walker> state)
        {
            return 1f;
        }
        public override string Descr(GoapActionArgs args)
        {
            var from = args.GetValue<Vector2>("From");
            var to = args.GetValue<Vector2>("To");
            return $"Taking left step from {from} to {to}";
        }
        public override GoapActionArgs ApplyToState(GoapState<Walker> state)
        {
            var homePos = state.GetFluent<Vector2>(_homePosition.Name).Value;
            var currentPos = state.GetFluent<Vector2>(_currentPosition.Name).Value;
            var strideLength = state.GetFluent<float>(_strideLength.Name).Value;
            float effectiveStrideLength = Mathf.Min(strideLength, currentPos.DistanceTo(homePos));
            Vector2 newPos = currentPos + (homePos - currentPos).Normalized() * effectiveStrideLength;
            state.MutateFluent(_currentPosition, newPos);
            state.MutateFluent(_leftFootForward, true);

            var args = new GoapActionArgs();
            args.AddArg("From", currentPos);
            args.AddArg("To", newPos);
            return args;
        }
    }
}