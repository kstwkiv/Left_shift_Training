using System;
using System.Reflection;
public delegate void MyDelegate(string Message);
public class HelloWorld
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Try Programiz.Pro");
        Type t=typeof(MyDelegate);
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