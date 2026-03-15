using System;
public class Person
{
    public int Id{get;set;}
    public string Name{get;set;}
    public string Phone{get;set;}
    public Person(int id,string name,string phone)
    {
        Id=id;
        Name=name;
        Phone=phone;
    }
}
public interface IBillable
{
    double CalculateBill(double baseAmount);
}
public class Doctor : Person,IBillable
{
    
}