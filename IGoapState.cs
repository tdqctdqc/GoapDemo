using Godot;
using System;

public interface IGoapState
{
    IGoapVarInstance GetVarGeneric(IGoapVar match);
}
