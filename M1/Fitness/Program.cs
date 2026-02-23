using System;
using System.Collections.Generic;
class MemberStack
{
    public string Name{get;set;}
    public int[] Weeks=new int[4];
    public MemberStack(string name,int[] weeks)
    {
        Name=name;
        Weeks=weeks;
    }
}
class Program
{
    static List<MemberStack>members=new List<MemberStack>();
    public static void RegisterMembers(MemberStack record)
    {
        members.Add(record);
    }
    public static Dictionary<string, int> GetHighestSteps(List<MemberStack> members,int threshold)
    {
        Dictionary<string,int> res=new Dictionary<string, int>();
        foreach(MemberStack m in members)
        {
            int count=0;
            for(int i=0;i<4;i++)
            {if (m.Weeks[i]>= threshold)
            {
                count++;
            }}
            
            res[m.Name]=count;
        }
        
        return res;
    }
    public static double CalculateAverageSteps()
    {
        return members.SelectMany(m=>m.Weeks).DefaultIfEmpty(0).Average();
    }
    public static void Main()
    {
        while (true)
        {
            int choice=int.Parse(Console.ReadLine());
            
            switch (choice)
            {
                case 1:
                string name=Console.ReadLine();
                int[] weeks=new int[4];
                for(int i = 0; i < 4; i++)
                    {
                        weeks[i]=int.Parse(Console.ReadLine());
                    }
                RegisterMembers(new MemberStack(name,weeks));
                Console.WriteLine("Members Registered.");
                break;
                case 2:
                int threshold=int.Parse(Console.ReadLine());
                Dictionary<string,int>Highsteps=new Dictionary<string, int>(GetHighestSteps(members,threshold));
                foreach(var item in Highsteps)
                    {
                        Console.WriteLine($"{item.Key} {item.Value}");
                    }                
                break;
                case 3:
                double avg=CalculateAverageSteps();
                Console.WriteLine($"average steps: {avg}");
                break;
                case 4:
                return;
            }
        }
    }
}