using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Dictionary<string, int> marks = new Dictionary<string, int>
        {
            { "Asha", 78 },
            { "Bala", 82 }
        };

        string student = Console.ReadLine();
        int newMark = int.Parse(Console.ReadLine());
        // TODO: Add or update mark
        if (marks.ContainsKey(student))
        {
            marks[student]=newMark;
            Console.WriteLine("Marks updated");
        }
        else{marks.Add(student,newMark);
        Console.WriteLine("new student added");}
        foreach(KeyValuePair<string,int> kvp in marks)
        {
            Console.WriteLine($"{kvp.Key} - {kvp.Value}");
        }
    }
}