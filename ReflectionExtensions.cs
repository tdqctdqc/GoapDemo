using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class ReflectionExtensions 
{
    public static List<TField> GetFieldsWithAttribute<TAttribute, TField>(this IEnumerable<FieldInfo> varFields)
        where TAttribute : System.Attribute
    {
        return varFields
            .Where(f => f.GetCustomAttribute<TAttribute>() != null)
            .Select(
                f => (TField)f.GetValue(null)  )
            .ToList();
    }
}
