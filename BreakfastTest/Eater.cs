using Godot;
using System;

public class Eater 
{
    public bool Hungry { get; private set; }
    public bool Caffeinated { get; private set; }
    public Bread Bread { get; private set; }
    public Coffee Coffee { get; private set; }

    public Eater()
    {
        Hungry = true;
        Caffeinated = false;
        Bread = new Bread();
        Coffee = new Coffee();
    }
}
