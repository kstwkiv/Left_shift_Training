using System;
namespace TopBrainsAssignments
{
    class MulTable
    {
        public static int[] table(int n,int upto)
        {
            int[] res=new int[upto];
            for(int i = 0; i < upto; i++)
            {
                res[i]=n*(i+1);
            }

            return res;
        }
    }
}