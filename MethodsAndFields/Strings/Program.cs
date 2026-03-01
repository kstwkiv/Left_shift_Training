using System;
using System.Reflection;
public class HelloWorld
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Try Programiz.pro");
        Type t=typeof(string);
        foreach(MethodInfo i in t.GetMethods())
        {
            Console.WriteLine(i);
        }
        foreach(FieldInfo f in t.GetFields())
        {
            Console.WriteLine(f);
        }
    }
}