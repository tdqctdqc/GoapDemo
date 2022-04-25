using Godot;
using System;

public interface IGoapAction : IGoapRuleFollower
{
    string Name { get; }
}
