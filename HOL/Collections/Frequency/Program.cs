using System;
class Program
{
    static void Main(string[] args)
    {
        string name=Console.ReadLine();
        char ch='a';
        int count=0;
        for(int i = 0; i < name.Length; i++)
        {
            if (name[i] == ch)
            {
                count++;
            }
        }
        Console.WriteLine("Frequency of "+ch+" in "+name +" is "+count);
    }
}