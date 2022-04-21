using Godot;

namespace GoapDemo.WalkHomeTest
{
    public class Walker
    {
        public Vector2 HomePosition { get; private set; }
        public Vector2 CurrentPosition { get; private set; }
        public bool LeftFootForward { get; private set; }
        public float StrideLength { get; private set; }

        public Walker(Vector2 homePos, Vector2 currentPos, bool leftFootStartsForward, float strideLength)
        {
            HomePosition = homePos;
            CurrentPosition = currentPos;
            LeftFootForward = leftFootStartsForward;
            StrideLength = strideLength;
        }
    }
}
