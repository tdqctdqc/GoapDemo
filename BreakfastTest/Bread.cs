using Godot;
using System;

public class Bread 
{
    public bool Toasted { get; private set; }
    public bool Buttered { get; private set; }

    public Bread()
    {
        Toasted = false;
        Buttered = false;
    }
}
