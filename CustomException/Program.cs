using System;
public class InvalidAgeException : Exception
{
    public InvalidAgeException(string message):base(message){}
}
class Program
{
    static void checkAge(int age)
    {
        if (age < 18)
        {
            throw new InvalidAgeException("Age must be 18 or above");
        }
        Console.WriteLine("Age is valid.");
    }
    static void Main()
    {
        try
        {
            checkAge(25);
        }
        catch(InvalidAgeException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}