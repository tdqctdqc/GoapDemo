using Godot;
using System;

public class Division 
{
    public Division(Vector2 position, Vector2 facing)
    {
        Position = position;
        Facing = facing;
    }

    public Vector2 Position { get; private set; }
    public Vector2 Facing { get; private set; }
}
