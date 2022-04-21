using System;
using System.Collections.Generic;
using Godot;

namespace GoapDemo.WalkHomeTest
{
    public class LeftStepAction : GoapAction<Walker>
    {
        private static GoapVar<Vector2, Walker> _currentPosition
            = Vec2Var<Walker>.ConstructScaledHeuristic("CurrentPosition", 1f, w => w.CurrentPosition);
        private static GoapVar<Vector2, Walker> _homePosition
            = Vec2Var<Walker>.ConstructScaledHeuristic("HomePosition", 1f, w => w.HomePosition);
        private static GoapVar<bool, Walker> _leftFootForward
            = BoolVar<Walker>.Construct("LeftFootForward", 1f, w => w.LeftFootForward);
        private static GoapVar<float, Walker> _strideLength
            = FloatVar<Walker>.ConstructScaleHeuristic("StrideLength", 1f, w => w.StrideLength);
        public LeftStepAction() : base("LeftStep")
        {
        }

        protected override void SetupVars()
        {
            Vars = new List<IGoapAgentVar<Walker>>
            {
                _currentPosition,
                _homePosition,
                _leftFootForward
            };
        }

        public override GoapState<Walker> TransformContextForSuccessorGoal(GoapState<Walker> actionContext)
        {
            return null;
        }

        public override GoapGoal<Walker> GetSuccessorGoal(GoapActionArgs args)
        {
            return null;
        }

        public override bool Valid(GoapState<Walker> state)
        {
            return state.GetVar<bool>(_leftFootForward.Name).Value == false;
        }

        public override float Cost(GoapState<Walker> state)
        {
            return 1f;
        }

        public override string Descr(GoapActionArgs args)
        {
            var from = args.GetValue<Vector2>("From");
            var to = args.GetValue<Vector2>("To");
            var str = new StringStruct();
            return $"Taking left step from {from} to {to}";
        }

        public override GoapActionArgs ApplyToState(GoapState<Walker> state)
        {
            var homePos = state.GetVar<Vector2>(_homePosition.Name).Value;
            var currentPos = state.GetVar<Vector2>(_currentPosition.Name).Value;
            var strideLength = state.GetVar<float>(_strideLength.Name).Value;
            float effectiveStrideLength = Mathf.Min(strideLength, currentPos.DistanceTo(homePos));
            Vector2 newPos = currentPos + (homePos - currentPos).Normalized() * effectiveStrideLength;
            state.MutateVar(_currentPosition, newPos);
            state.MutateVar(_leftFootForward, true);


            var args = new GoapActionArgs();
            args.AddArg("From", currentPos);
            args.AddArg("To", newPos);
            return args;
        }
    }

    public struct StringStruct
    {
        public string Value { get; private set; }

        public StringStruct(string value)
        {
            Value = value;
        }
    }
}