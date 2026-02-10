using System;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter word1: ");
        string word1=Console.ReadLine();
        Console.WriteLine("Enter word2: ");
        string word2=Console.ReadLine();
        Dictionary<char,int> f1=new Dictionary<char, int>();
        Dictionary<char,int>f2=new Dictionary<char, int>();
        foreach(char ch in word1)
        {
            if (f1.ContainsKey(ch))
            {
                f1[ch]++;
            }
            else
            {
                f1[ch]=1;
            }
        }

        foreach(char ch in word2)
        {
            if (f2.ContainsKey(ch))
            {
                f2[ch]++;
            }
            else
            {
                f2[ch]=1;
            }
        }
        int counts=0;
        foreach(char ch in f1.Keys)
        {
            if (f2.ContainsKey(ch))
            {
                if (f1[ch] > f2[ch])
                {
                    counts+=f1[ch]-f2[ch];
                }
            }
            else
            {
                counts+=f1[ch];
            }
        }

        Console.WriteLine("Chars to dlt from word1: "+counts);
    }
}