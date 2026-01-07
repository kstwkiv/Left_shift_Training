using System;
namespace SmallAssignments
{
    class SumPos
    {
        public static int Summation(int[] nums)
        {
            int sum=0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == 0)
                {
                    break;
                }

                if (nums[i] < 0)
                {
                    continue;
                }

                sum+=nums[i];
                
            }

            return sum;
        }
    }
}