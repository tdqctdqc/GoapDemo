using Godot;
using System;
using System.Reflection;
using System.Collections.Generic;
using GoapDemo.BreakfastTest;
using GoapDemo.WalkHomeTest;

public class Root : Node
{
    public override void _Ready()
    {
        DoWalkerTest();
        DoBreakfastTest();
        // DoReflectionTest();
    }

    private void DoBreakfastTest()
    {
        var eater = new Eater();
        var agent = new EaterAgent(eater);
        var list = new List<GoapAgent<Eater>> {agent};
        var goal = new DoBreakfastGoal();
        GoapPlanner.PlanGoal(goal, list);
    }

    private void DoWalkerTest()
    {
        var walker = new Walker(Vector2.One * 100f, Vector2.Zero, true, 10f);
        var agent = new WalkerAgent(walker);
        var list = new List<GoapAgent<Walker>> {agent};
        var goal = new WalkHomeGoal();
        GoapPlanner.PlanGoal(goal, list);
    }

    private void DoReflectionTest()
    {
        var type1 = typeof(ReflectionTestClass1);
        // var staticNonPublicFields = type1.GetFields(BindingFlags.NonPublic | BindingFlags.Static);
        // GD.Print("number of static non public fields: " + staticNonPublicFields.Length);
        // var instanceNonPublicFields = type1.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        // GD.Print("number of instance non public fields: " + instanceNonPublicFields.Length);
        // var staticPublicFields = type1.GetFields(BindingFlags.Public | BindingFlags.Static);
        // GD.Print("number of static public fields: " + staticPublicFields.Length);
        // var instancePublicFields = type1.GetFields(BindingFlags.Public | BindingFlags.Instance);
        // GD.Print("number of instance public fields: " + instancePublicFields.Length);
        
        var fields = type1.GetAllFields();
        GD.Print("total number of fields: " + fields.Length);
        for (int i = 0; i < fields.Length; i++)
        {
            GD.Print(fields[i].Name);
        }

        // var privFields = type1.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        // GD.Print("total number of private fields: " + privFields.Length);
        // for (int i = 0; i < privFields.Length; i++)
        // {
        //     GD.Print(privFields[i].Name);
        // }
    }

    private class ReflectionTestClass1
    {
        public static float PubStaticVar1;
        
        public float PubVar1;
        
        private static float _privStaticVar1;
        private static float _privStaticVar2;
        private static float _privStaticVar3;
        
        private float _privVar1;
        private float _privVar2;
    }
}
