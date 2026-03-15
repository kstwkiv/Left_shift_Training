using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string[] attempts = { "raj", "kavi", "raj", "raj", "kavi" };
        Dictionary<string, int> failedAttempts = new Dictionary<string, int>();
        // TODO: Count attempts and print users with attempts >= 3
        for(int i = 0; i < attempts.Length; i++)
        {
            string name=attempts[i];
            if (failedAttempts.ContainsKey(name))
            {
                failedAttempts[name]+=1;
            }
            else
            {
                failedAttempts[name]=1;
            }
        }

        foreach(KeyValuePair<string,int> kvp in failedAttempts)
        {
            if (kvp.Value >= 3)
            {   
                Console.WriteLine(kvp.Key);
            }
        }
    }
}