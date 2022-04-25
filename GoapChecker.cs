using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

public static class GoapChecker 
{
    public static void CheckRules()
    {
        var types = Assembly.GetExecutingAssembly().GetTypes();
        var goalTypes = types.Where(t => typeof(IGoapGoal).IsAssignableFrom(t)).ToList();
        var actionTypes = types.Where(t => typeof(IGoapAction).IsAssignableFrom(t)).ToList();
        
        CheckRuleFollower<IGoapGoal>(goalTypes);
        CheckRuleFollower<IGoapAction>(actionTypes);
    }

    private static void CheckRuleFollower<TRuleFollower>(List<Type> types) 
        where TRuleFollower : IGoapRuleFollower
    {
        foreach (var type in types)
        {
            if (type.IsAbstract || type.IsInterface) continue; 
            var testCase = (TRuleFollower) type.GetAllMethods()
                .WithAttribute<MethodInfo, TestCaseAttribute>()
                .First()
                .Invoke(null, null);
            testCase.CheckRules();
        }
    }
    public static void CheckActionSuccessorVars<TAgent>(GoapAction<TAgent> action)
    {
        var goal = action.GetSuccessorGoal(new GoapActionArgs());
        if (goal == null) return;
        
        foreach (var goalVar in goal.ExplicitVars)
        {
            var successorVars =
                action.GetValuesForMembersWithAttributeType<IGoapAgentVar<TAgent>, SuccessorVarAttribute>();

            if ( successorVars.Any(v => v.Name == goalVar.Name
                                         && v.ValueType == goalVar.ValueType) == false )
            { throw new Exception("can't fulfil successor goal var " + goalVar.Name); }
        }
    }
    
    public static void CheckActionVars<TAgent>(GoapGoal<TAgent> goal)
    {
        foreach (var subGoal in goal.SubGoals)
            foreach (var action in subGoal.Actions)
                foreach (var actionVar in action.ExplicitVars)
                    if ( goal.ExplicitVars.Any(v => v.Name == actionVar.Name
                                        && v.ValueType == actionVar.ValueType)
                            == false )
                    { throw new Exception("can't fulfil action var " + actionVar.Name); }
    }

    public static void CheckImplicitVars<TAgent>(GoapGoal<TAgent> goal)
    {
        foreach (var implicitVar in goal.ImplicitVars)
        {
            var implicitVarDependencies = implicitVar
                .GetValuesForMembersWithAttributeType<IGoapAgentVar<TAgent>, ExplicitVarAttribute>();
            foreach (var dependency in implicitVarDependencies)
            {
                if ( goal.ExplicitVars .Any(v => v.Name == dependency.Name
                                    && v.ValueType == dependency.ValueType)
                        == false )
                { throw new Exception("can't fulfil implicit var " + implicitVar.Name); }
            }
        }
    }
}
