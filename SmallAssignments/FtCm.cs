using System;
namespace SmallAssignments
{
    class FtCm
    {
        static double rate=30.48;
        public static double Converter(int feet)
        {
            double cm=feet*rate;
            double answer=Math.Round(cm,2,MidpointRounding.AwayFromZero);

            return answer;
        }
    }
}