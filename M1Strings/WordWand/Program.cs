using System;
class Program
{
    
    public static bool CheckWords(string input)
    {
        foreach(char ch in input)
        {
            if (!(char.IsLetter(ch) || ch==' '))
            {
                return false;
            }
        }
        return true;
    }
    public static string ReverseWord(string word)
    {
        char[] arr=word.ToCharArray();
        Array.Reverse(arr);
        return new string(arr);
    }
    static void Main(String[] args)
    {
        Console.WriteLine("Enter the input:");
        string input=Console.ReadLine();
        if (!CheckWords(input))
        {
            Console.WriteLine("Invalid Sentence");
            return;
        }

        string[] words=input.Split(' ');
        int wordcount=words.Length;
        Console.WriteLine("Words Count: "+wordcount);

        if (wordcount % 2 == 0)
        {
            Array.Reverse(words);
            Console.WriteLine(string.Join(" ",words));
        }
        else
        {
            for(int i = 0; i < words.Length; i++)
            {
                words[i]=ReverseWord(words[i]);
            }
            Console.WriteLine(string.Join(" ",words));
        }
    }
}