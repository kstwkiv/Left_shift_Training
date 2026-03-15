using System;
class Program
{
    static void Main()
    {
        try
        {
            int n1=10;
            int n2=0;

            int res=n1/n2;
            Console.WriteLine(res);
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("Division by zero is not allowed.");
        }
        finally
        {
            Console.WriteLine("Program finished execution");
        }
    }
}