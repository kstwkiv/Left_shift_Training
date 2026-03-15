using System;
public class InvalidFlavourException : Exception
{
    public InvalidFlavourException(string message) : base(message)
    {
    }
}
public class Cake
{
    public string Flavour{ get;set; } 
    public int QuantityInKG{ get;set; } 
    public double PricePerKG{ get;set; } 
    public bool CakeOrder()
    {
        if (Flavour!="Red Velvet" && Flavour!="Chocolate" && Flavour != "Vanilla")
        {
            throw new InvalidFlavourException("Flavour Not Available!");
        }
        

        if (QuantityInKG <=0)
        {
            throw new Exception("Quantity must be higher!!");
        }

        return true;
    }
    public double PriceCalc()
    {
        double disc=0;
        if (Flavour == "Vanilla")
        {
            disc=3;
        }else if (Flavour == "Chocolate")
        {
            disc=7;
        }else if(Flavour=="Red Velvet")
        {
            disc=9;
        }

        double totalPrice=QuantityInKG*PricePerKG;
        double discPrice=totalPrice-(totalPrice*disc/100);
        return discPrice;
    }
}

class Program
{
    public static void Main(string[] args)
    {
        Cake cake=new Cake();
        Console.WriteLine("Enter the Flavour");
        cake.Flavour=Console.ReadLine();
        
        Console.WriteLine("Enter the weight");
        cake.QuantityInKG=int.Parse(Console.ReadLine());

        Console.WriteLine("Enter the price per kg");
        cake.PricePerKG=double.Parse(Console.ReadLine());

        try
        {
            if (cake.CakeOrder())
            {
                Console.WriteLine("Cake Ordered Successfully!");
                double price=cake.PriceCalc();
                Console.WriteLine("The caker price is "+price);
            }
        }
        catch(InvalidFlavourException e)
        {
            Console.WriteLine(e.Message);
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}