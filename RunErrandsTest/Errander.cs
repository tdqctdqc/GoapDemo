using Godot;
using System;

public class Errander 
{
    public Errander(Vector2 homePosition)
    {
        HomePosition = homePosition;
        CurrentPosition = homePosition;
        NeedToReturnBooks = true;
        NeedToCashCheck = true;
        NeedToBuyGroceries = true;
    }

    public Vector2 HomePosition { get; private set; }
    public Vector2 CurrentPosition { get; private set; }
    public bool NeedToReturnBooks { get; private set; }
    public bool NeedToCashCheck { get; private set; }
    public bool NeedToBuyGroceries { get; private set; }
}
