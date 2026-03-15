using System;
namespace SmallAssignments
{
    class Category
    {
        public static string HtCat(int height)
        {
            if (height < 150)
            {
                return "Short";
            }else if (height>=150 && height < 180)
            {
                return "Average";
            }
            else
            {
                return "Tall";
            }
        }
    }
}