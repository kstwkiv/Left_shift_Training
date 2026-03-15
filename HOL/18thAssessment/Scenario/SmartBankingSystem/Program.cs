using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
public class InsufficientBalanceException : Exception
{
    public InsufficientBalanceException(string Message):base(Message){}
}
public class MinimumBalanceException : Exception
{
    public MinimumBalanceException(string Message):base(Message){}
}
public class InvalidTransactionException : Exception
{
    public InvalidTransactionException(string Message):base(Message){}
}
public abstract class BankAccount
{
    public List<string> TransactionHistory=new List<string>();

    protected string accountNumber;
    protected string customerName;
    protected double balance;
    public string AccountNumber
    {
        get{return accountNumber;}
        set{accountNumber=value;}
    }
    public string CustomerName
    {
        get{return customerName;}
        set{customerName=value;}
    }
    public double Balance
    {
        get{return balance;}
        set{balance=value;}
    }
    public abstract double CalculateInterest();
    public virtual void Deposit(double amount)
    {
        if (amount <= 0)
        {
            throw new InvalidTransactionException($"Deposit must be +ve");
        }
        Balance+=amount;
        TransactionHistory.Add($"Deposit {amount}");
    }
    public virtual void Withdraw(double amount)
    {
        if (amount <=0)
        {
            throw new InvalidTransactionException($"Amount to withdraw is greater than Balance");
        }

        if (amount >Balance)
        {
            throw new InsufficientBalanceException($"Amount must be positive");
        }

        Balance-=amount;
        TransactionHistory.Add($"Withdrawn : {amount}");
    }
    public BankAccount(string accNo,string cusName,double balance){
        this.accountNumber=accNo;
        this.customerName=cusName;
        this.balance=balance;
    }

}
class SavingsAccount : BankAccount
{
    private const double MinimumBalance=1000;
    public SavingsAccount(string accNo,string cusName,double balance) : base(accNo, cusName, balance)
    {
        
    }
    public override void Withdraw(double amount)
    {
        if (Balance - amount < MinimumBalance)
        {
            throw new MinimumBalanceException($"minimum savings voilated");
        }
        
        base.Withdraw(amount);
    }
    public override double CalculateInterest()
    {
        return Balance*0.04;
    }
}
class CurrentAccount : BankAccount
{
    private const double OverDraftLimit=20000;
    public CurrentAccount(string accNo,string cusName,double amount) : base(accNo, cusName, amount)
    {
        
    }
    public override void Withdraw(double amount)
    {
        base.Withdraw(amount);
    }
    public override double CalculateInterest()
    {
        return 0;
    }
}
class LoanAccount : BankAccount
{
    public LoanAccount(string accNo,string cusName,double balance) : base(accNo, cusName, balance)
    {
        
    }
    public override void Withdraw(double amount)
    {
        throw new InvalidTransactionException("Cannot deposit into laon account");
    }
    public override double CalculateInterest()
    {
        return balance*0.10;
    }
}
class Program
{
    static List<BankAccount> accounts=new List<BankAccount>();
    static void Main()
    {
        accounts.Add(new SavingsAccount("A101", "Ravi", 80000));
        accounts.Add(new CurrentAccount("A102", "Sneha", 40000));
        accounts.Add(new LoanAccount("A103", "Rohan", 120000));
        accounts.Add(new SavingsAccount("A104", "Ritika", 55000));
        accounts.Add(new CurrentAccount("A105", "Raj", 90000));
        while (true)
        {
            Console.WriteLine("1. Deposit 2. withdraw 3. Reports 4.Exit");
            int choice=int.Parse(Console.ReadLine());
            try
            {
                switch (choice)
                {
                    case 1: Deposit(); break;
                    case 2: Withdraw(); break;
                    case 3: Reports(); break;
                    case 4: return;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: "+e.Message);
            }
        }
    }
    static BankAccount Find(string accNo)
    {
        return accounts.FirstOrDefault(a=>a.AccountNumber==accNo);
    }
    static void Deposit()
    {
        Console.WriteLine("Account: ");
        var acc=Find(Console.ReadLine());
        Console.Write("Amount: ");
        acc.Deposit(double.Parse(Console.ReadLine()));
    }
    static void Withdraw()
    {
        Console.WriteLine("Account: ");
        var acc=Find(Console.ReadLine());
        Console.WriteLine("Amount: ");
        acc.Withdraw(double.Parse(Console.ReadLine()));
    }
    static void Reports()
    {
        var rich=accounts.Where(a=>a.Balance>50000);
        foreach(var r in rich)
        {
            Console.WriteLine(r.CustomerName);
        }

        Console.WriteLine(accounts.Sum(a=>a.Balance));
        var top3=accounts.OrderByDescending(a=>a.Balance).Take(3);
        foreach(var a in top3){
            Console.WriteLine(a.CustomerName);
        }
        var grouped=accounts.GroupBy(a=>a.GetType().Name);
        foreach(var h in grouped)
        {
            foreach(var g in h)
            {
                Console.WriteLine(" "+g.CustomerName);
            }
        }
        var rnames=accounts.Where(a=>a.CustomerName.StartsWith('R'));
        foreach(var a in rnames)
        {
            Console.WriteLine(a.CustomerName);
        }
    }
}
