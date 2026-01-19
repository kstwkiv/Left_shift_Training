using System;
using System.Collections.Generic;
using System.Text;

namespace TopBrainsAssignments
{
    class AandV
    {
        static bool isVowel(char c)
        {
            c=char.ToLower(c);
            return c=='a'||c=='e'||c=='i'||c=='o'||c=='u';
        }
        public static string Check(string s1,string s2)
        {
            HashSet<char> cs2=new HashSet<char>();
            foreach(char c in s2)
            {
                char lowerChar=char.ToLower(c);
                if (!isVowel(lowerChar))
                {
                    cs2.Add(lowerChar);
                }
            }

            StringBuilder filtered=new StringBuilder();
            foreach(char c in s1)
            {
                char lowerChar=char.ToLower(c);
                if (isVowel(lowerChar) || !cs2.Contains(lowerChar))
                {
                    filtered.Append(c);
                }
            }

            StringBuilder result=new StringBuilder();
            for(int i = 0; i < filtered.Length; i++)
            {
                if (i == 0 || char.ToLower(filtered[i]) != char.ToLower(filtered[i - 1]))
                {
                    result.Append(filtered[i]);
                }
            } 
            return result.ToString();
        }
    }
}