using Godot;
using System;

namespace GoapDemo.WalkHomeTest
{
    public class WalkerAgent : GoapAgent<Walker>
    {
        public WalkerAgent(Walker entity) : base(entity)
        {
        }
    }
}

