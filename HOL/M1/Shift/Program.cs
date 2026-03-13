using System;
using System.Text;
class Program
{
    static void Main(string[] args)
    {
        string input=Console.ReadLine();
        string text='"'+input+'"';
        bool flag=true;
        if (input.Length <= 4)
        {
            flag=false;
            Console.WriteLine($"The string {text} has minimum length.");
        }
        
        string temp="";
        foreach(char ch in input)
        {
            if(ch==' ')
            {
                flag=false;
                Console.WriteLine($"The string {text} contains space.");
            }
            int ascii=(int)ch;
            int shift=ascii-10;
            temp+=(char)shift;
        }
        
        string res=temp;
        if (flag)
        {
            Console.WriteLine(res);
        }
        
    }
}