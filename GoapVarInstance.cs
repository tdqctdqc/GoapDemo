using Godot;
using System;

public class GoapVarInstance<TValue, TAgent>
{
    public GoapVar<TValue,TAgent> BaseVar { get; private set; }
    public TValue Value  { get; private set; }
    public Type ValueType => BaseVar.ValueType;
    public string Name => BaseVar.Name;
}
