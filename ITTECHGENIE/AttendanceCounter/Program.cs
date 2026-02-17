using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        int[] employeeIds = { 1001, 1002, 1001, 1003, 1002, 1001 };
        Dictionary<int, int> attendance = new Dictionary<int, int>();
        // TODO: Build frequency map and print
        //HashSet<int>freq=new HashSet<int>();
        for(int i = 0; i < employeeIds.Length; i++)
        {
            int id=employeeIds[i];
            if (attendance.ContainsKey(id))
            {
                attendance[id]+=1;
            }
            else
            {
                attendance[id]=1;
            }
        }
        foreach(KeyValuePair<int,int> kvp in attendance)
        {
            Console.WriteLine($"{kvp.Key} - {kvp.Value}");
        }
    }
}
