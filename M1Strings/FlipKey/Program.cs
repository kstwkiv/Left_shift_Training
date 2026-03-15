using System;
class Program
{
    public static string Cleanse(string Word)
    {
        if (string.IsNullOrEmpty(Word) || Word.Length < 6)
        {
            return "Invalid Input";
        }

        foreach(char ch in Word)
        {
            if(!char.IsLetter(ch))
            {
                return "Invalid Input";
            }
        }

        Word=Word.ToLower();
        string output="";
        for(int i = 0; i < Word.Length; i++)
        {
            if (((int)Word[i]) % 2 != 0)
            {
                output+=Word[i];
            }
        }
        char[] arr=output.ToCharArray();
        Array.Reverse(arr);
        for(int i = 0; i < arr.Length; i++)
        {
            if (i % 2 == 0)
            {
                arr[i]=char.ToUpper(arr[i]);
            }
        }
        return new string(arr);
    }
    static void Main(string[] args)
    {
        string word=Console.ReadLine();
        string res=Cleanse(word);
        Console.WriteLine("The generated key is - "+res);
    }
}