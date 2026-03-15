using System;
namespace SmallAssignments
{
    public class CircleArea
{
    public static double Area(double radius)
    {
        double area=Math.PI*radius*radius;
        return Math.Round(area,2,MidpointRounding.AwayFromZero);
         
        }

}
}