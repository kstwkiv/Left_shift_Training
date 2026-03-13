using System;
class Program
{
    static void Main(string[] args)
    {
        string input=Console.ReadLine();
        int index=input.IndexOf('@');
        string username=input.Substring(0,index);
        string domain=input.Substring(index);
        Console.WriteLine(username[0]+"***"+domain);
    }
}