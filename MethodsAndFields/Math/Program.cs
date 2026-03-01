using System;
using System.Reflection;
public class HelloWorld
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Try Programiz.Pro");
        Type t=typeof(Math);
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