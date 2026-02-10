using System;
class Program
{
    public static bool IsValid(string username)
    {
        return username.Length==8;
    }
    public static bool IsCourse(string username)
    {
        string last=username.Substring(username.Length-3);
        int course;

        if(!int.TryParse(last,out course))
        {
            return false;
        }

        return course>=101 && course<=115;
    }
    public static string GP(string username)
    {
        string pwd="TECH_";
        string value=ascii(username.Substring(0,4));
        pwd+=value;
        if (IsCourse(username))
        {
            string ans=username.Substring(username.Length-3);
            pwd+=ans;
        }
    }
    public static string ascii(string word)
    {
        int add=0;
        foreach(char ch in word)
        {
            add+=(int)ch;
        }
        return add.ToString();
    }

 static void Main(string[] args)
    {
        string username=Console.ReadLine();
        if (!IsValid(username) || !IsCourse(username))
        {
            Console.WriteLine(username+" is an invalid username");
        }
        else
        {
            Console.WriteLine("Password: "+GP(username));
        }
    }   
}