using System;
class Analysis
{
    public void Display(string HRS,string MS)
    {
        double hrs;
        double ms;
        bool validhrs=double.TryParse(HRS,out hrs);
        bool validms=double.TryParse(MS,out ms);
        if (!validhrs || !validms)
        {
            Console.WriteLine("Invalid Input");
            return;
        }

        double overall=(hrs*hrs)+(ms*ms);
        if (overall > 100)
        {
            Console.WriteLine("Heart Rate Score: "+HRS+", Movement Score : "+MS);
        }
        else
        {
            Console.WriteLine("Overall Intensity : "+overall);
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        string HRS=Console.ReadLine();
        string MS=Console.ReadLine();
        Analysis obj=new Analysis();
        obj.Display(HRS,MS);
    }
}