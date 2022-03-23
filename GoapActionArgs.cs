using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class GoapActionArgs
{
    private Dictionary<string, GoapActionArg> _vars;
    public GoapActionArgs()
    {
        _vars = new Dictionary<string, GoapActionArg>();
    }

    public GoapActionArg GetArg<T>(string name)
    {
        if (_vars.ContainsKey(name) == false) return null;
        if (_vars[name].Value is T t) return _vars[name];
        return null;
    }
    public void AddArg(string name, object value)
    {
        _vars.Add(name, new GoapActionArg(name, value));
    }

    public class GoapActionArg
    {
        public string Name { get; private set; }
        public object Value { get; private set; }

        public GoapActionArg(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}
