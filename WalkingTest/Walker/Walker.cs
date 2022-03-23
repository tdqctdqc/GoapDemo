using Godot;
using System;

public class Walker
{
    public Vector2 HomeLocation = Vector2.One * 100f;
    public Vector2 Location = Vector2.Zero;
    public float StrideLength = 10f;
    public bool LeftFootForward = false; 
}
