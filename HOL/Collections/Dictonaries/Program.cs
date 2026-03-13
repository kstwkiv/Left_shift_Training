using System;
using System.Collections.Generic;
class Program
{
    static void Main()
    {
        Dictionary<string,int>Student=new Dictionary<string, int>();
        Console.WriteLine("Enter details");
        Student.Add("Sathwika",11);
        Student.Add("Samindra",13);
        foreach(var items in Student)
        {
            Console.WriteLine(items.Key+":"+items.Value);
        }
        foreach(KeyValuePair<string,int> item in Student)
        {
            Console.WriteLine(item.Key +" "+item.Value);
        }
        foreach(int val in Student.Values) //coz my values are integers
        {
            Console.WriteLine(val);
        }
        foreach(string k in Student.Keys)
        {
            Console.WriteLine(k);
        }
    }
}