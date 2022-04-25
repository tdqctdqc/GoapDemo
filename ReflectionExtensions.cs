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
        var fields = GetAllFields(type).WithAttribute<FieldInfo, TAttribute>();
        var props = GetAllProperties(type).WithAttribute<PropertyInfo, TAttribute>();
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
    public static List<TResult> GetResultsForMethods<TResult>(this IEnumerable<MethodInfo> methodInfos, object instance, object[] args = null)
    {
        return methodInfos
            .Select
            ( p =>
                {
                    var valueHaver = p.IsStatic ? null : instance;
                    return (TResult) p.Invoke(valueHaver, args);
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
    public static MethodInfo[] GetAllMethods(this Type type)
    {
        return type.GetMethods(BindingFlags.Instance
                                  | BindingFlags.Static
                                  | BindingFlags.Public
                                  | BindingFlags.NonPublic);
    }

    public static IEnumerable<TInfo> WithAttribute<TInfo, TAttribute>(this IEnumerable<TInfo> memberInfos) 
        where TInfo : MemberInfo where TAttribute : System.Attribute
    {
        return memberInfos.Where(f => f.GetCustomAttribute<TAttribute>() != null);
    }
}
