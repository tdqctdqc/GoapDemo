using Godot;
using System;

public interface IGoapState
{
    IGoapVar GetGenericVar(IGoapVar match);
}
