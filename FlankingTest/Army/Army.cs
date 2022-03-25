using Godot;
using System;

public class Army 
{
    public Vector2 Position { get; private set; }
    public Vector2 Facing { get; private set; }
    public Army Enemy { get; private set; }
    public bool IsEngaged { get; private set; }
    public bool IsFlanked { get; private set; }

    public Army(Vector2 pos, Vector2 facing)
    {
        Position = pos;
        Facing = facing;
        Enemy = new Army(this);
    }

    private Army(Army enemy)
    {
        Enemy = enemy; 
    }
}
