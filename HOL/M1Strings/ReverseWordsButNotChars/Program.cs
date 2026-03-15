using System;
class Program
{
    static void Main(string[] args)
    {
        string input=Console.ReadLine();
        string[] words=input.Split(' ');
        string output="";
        for(int i=words.Length-1;i>=0;i--)
        {
            output+=string.Join(" ",words[i]);
            if (i != 0)
            {
                output+=" ";
            }
        }

        Console.WriteLine(output);
    }
}