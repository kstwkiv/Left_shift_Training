using System;
using System.Security.Cryptography.X509Certificates;
public class Candy
{
    public string Flavour {get; set;}
    public int Quantity{get;set;}
    public int PricePerPeice{get;set;}
    public double TotalPrice{get; set;}
    public double Discount{get; set;}

    public bool ValidateCandyFlavour()
    {
        if (Flavour=="Strawberry" || Flavour=="Lemon" || Flavour == "Mint")
        {
            return true;
        }
        return false;
    }
}

public class Program
{
    public Candy CalculateDiscountPrice(Candy candy)
    {
        double disc=0;
        if (candy.Flavour == "Strawberry")
        {
            disc=10;
        }else if (candy.Flavour == "Lemon")
        {
            disc=12;
        }else if (candy.Flavour == "Mint")
        {
            disc=12;
        }

        candy.TotalPrice=candy.Quantity*candy.PricePerPeice;
        candy.Discount=candy.TotalPrice-(candy.TotalPrice*disc/100);

        return candy;
    }

    public static void Main(string[] args)
    {
        
    }
}