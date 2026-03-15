using System;
namespace SmallAssignments
{
    class iter
    {
        public static void Iteration(int[] arr)
        {
            int n=arr.Length;
            for(int i = 0; i < n; i++)
            {
                Console.WriteLine(arr[i]);
            }

            foreach(int num in arr)
            {
                Console.WriteLine(num);
            }
        }

    }
}