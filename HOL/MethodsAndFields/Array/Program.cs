using System;
using System.Reflection;
using System.Collections.Generic;
public class HelloWorld
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Try Programiz.Pro");
        Type t=typeof(Array);
        foreach(MethodInfo i in t.GetMethods())
        {
            Console.WriteLine(i);
        }
        foreach(FieldInfo j in t.GetFields())
        {
            Console.WriteLine(j);
        }
    }
}