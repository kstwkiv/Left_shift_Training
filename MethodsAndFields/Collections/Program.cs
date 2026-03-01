using System;
using System.Collections.Generic;
using System.Reflection;
public class HelloWorld
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Try Programiz.pro");
        Type t=typeof(List<int>);
        foreach(MethodInfo i in t.GetMethods())
        {
            Console.WriteLine(i);
        }
        foreach(FieldInfo i in t.GetFields())
        {
            Console.WriteLine(i);
        }
    }
}