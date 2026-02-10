using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Numerics;
namespace SmallAssignments{
class Program
{
    public static void Main(string[] args)
    {
        /*double radius=2.5;
        double answer=CircleArea.Area(radius);

        Console.WriteLine(answer);

        int feet=(int)6.2;
        double res=FtCm.Converter(feet);

        Console.WriteLine(res);

        int height=153;
        string checkHeight=Category.HtCat(height);
        Console.WriteLine(checkHeight);

        Console.WriteLine("enter values: a,b,c");
        int a=int.Parse(Console.ReadLine());
        int b=int.Parse(Console.ReadLine());
        int c=int.Parse(Console.ReadLine());
        Largest lar =new Largest();
        int largest=lar.Compare(a,b,c);
        Console.WriteLine("The largest value in "+a+" "+b+" "+c+" is : "+largest);
        

        int[] nums={3,1,-2,3,0,9,2,-1};
        int sum=SumPos.Summation(nums);
        Console.WriteLine(sum);*/

        int n=int.Parse(Console.ReadLine());
        int[] arr=new int[n];
        for(int i = 0; i < n; i++)
            {
                arr[i]=int.Parse(Console.ReadLine());
            }
        iter.Iteration(arr);
        
        Employee e1 = new Employee(101, "Sathwika", 60000);

        Employee e2 = new Employee(e1);

        e1.Display();
        e2.Display();

    }
}
}