using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class ReflectionExtensions 
{
    public static List<TField> GetValuesForFieldsWithAttribute<TAttribute, TField>(this IEnumerable<FieldInfo> varFields)
        where TAttribute : System.Attribute
    {
        return varFields
            .Where(f => f.GetCustomAttribute<TAttribute>() != null)
            .Select(
                f => (TField)f.GetValue(null)  )
            .ToList();
    }

    public static FieldInfo[] GetAllFields(this Type type)
    {
        return type.GetFields(BindingFlags.Instance 
                              | BindingFlags.Static 
                              | BindingFlags.Public 
                              | BindingFlags.NonPublic);
    }
}
