using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class ReflectionExtensions 
{
    public static List<TValue> GetValuesForMembersWithAttributeType<TValue, TAttribute>(this object ob)
        where TAttribute : System.Attribute
    {
        var type = ob.GetType();
        var fields = GetAllFields(type).WithAttribute<TAttribute>();
        var props = GetAllProperties(type).WithAttribute<TAttribute>();
        var result = GetValuesForFields<TValue>(fields, ob);
        result.AddRange(GetValuesForProperties<TValue>(props, ob));
        return result;
    }
    public static List<TField> GetValuesForFields<TField>(this IEnumerable<FieldInfo> varFields, object instance)
    {
        return varFields
            .Select
            ( f =>
                {
                    var valueHaver = f.IsStatic ? null : instance;
                    return (TField) f.GetValue(valueHaver);
                }
            )
            .ToList();
    }

    public static bool IsStatic(this PropertyInfo source)
    {
        return source.GetAccessors(false).Any(x => x.IsStatic);
    }
    public static List<TProperty> GetValuesForProperties<TProperty>(this IEnumerable<PropertyInfo> varProperties, object instance)
    {
        return varProperties
            .Select
            ( p =>
                {
                    var valueHaver = p.IsStatic() ? null : instance;
                    return (TProperty) p.GetValue(valueHaver);
                }
            )
            .ToList();
    }
    public static FieldInfo[] GetAllFields(this Type type)
    {
        return type.GetFields(BindingFlags.Instance 
                              | BindingFlags.Static 
                              | BindingFlags.Public 
                              | BindingFlags.NonPublic);
    }
    public static PropertyInfo[] GetAllProperties(this Type type)
    {
        return type.GetProperties(BindingFlags.Instance
                                       | BindingFlags.Static
                                       | BindingFlags.Public
                                       | BindingFlags.NonPublic);
    }
    public static IEnumerable<FieldInfo> WithAttribute<TAttribute>(this IEnumerable<FieldInfo> fields) where TAttribute : System.Attribute
    {
        return fields.Where(f => f.GetCustomAttribute<TAttribute>() != null);
    }
    public static IEnumerable<PropertyInfo> WithAttribute<TAttribute>(this IEnumerable<PropertyInfo> fields) where TAttribute : System.Attribute
    {
        return fields.Where(f => f.GetCustomAttribute<TAttribute>() != null);
    }
}
