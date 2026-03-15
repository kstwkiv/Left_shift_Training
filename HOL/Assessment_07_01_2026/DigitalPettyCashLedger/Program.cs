using System;
using System.Collections.Generic;

public interface Reportable
{
    string GetSummary();
}

public abstract class Transaction : Reportable
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }

    protected Transaction(int id, DateTime date, decimal amount, string description)
    {
        Id = id;
        Date = date;
        Amount = amount;
        Description = description;
    }

    public abstract string GetSummary();
}

public class IncomeTransaction : Transaction
{
    public string Source { get; set; }

    public IncomeTransaction(int id, DateTime date, decimal amount, string description, string source)
        : base(id, date, amount, description)
    {
        Source = source;
    }

    public override string GetSummary()
    {
        return "[INCOME] " + Date.ToShortDateString()+ " | $" + Amount+ " | Source: " + Source+ " | " + Description;
    }
}

public class ExpenseTransaction : Transaction
{
    public string Category { get; set; }

    public ExpenseTransaction(int id, DateTime date, decimal amount, string description, string category)
        : base(id, date, amount, description)
    {
        Category = category;
    }

    public override string GetSummary()
    {
        return "[EXPENSE] " + Date.ToShortDateString()+ " | $" + Amount+ " | Category: " + Category+ " | " + Description;
    }
}

public class Ledger<T> where T : Transaction
{
    private List<T> transactions = new List<T>();

    public void AddEntry(T entry)
    {
        transactions.Add(entry);
    }

    public List<T> GetTransactionsByDate(DateTime date)
    {
        List<T> result = new List<T>();

        for (int i = 0; i < transactions.Count; i++)
        {
            if (transactions[i].Date.Date == date.Date)
            {
                result.Add(transactions[i]);
            }
        }
        return result;
    }

    public decimal CalculateTotal()
    {
        decimal total = 0;

        for (int i = 0; i < transactions.Count; i++)
        {
            total += transactions[i].Amount;
        }
        return total;
    }

    public List<T> GetAll()
    {
        return transactions;
    }
}

class Program
{
    static void Main()
    {
        Ledger<IncomeTransaction> incomeLedger = new Ledger<IncomeTransaction>();
        incomeLedger.AddEntry(
            new IncomeTransaction(1, DateTime.Now, 500m, "Petty cash replenishment", "Main Cash")
        );

        Ledger<ExpenseTransaction> expenseLedger = new Ledger<ExpenseTransaction>();
        expenseLedger.AddEntry(
            new ExpenseTransaction(2, DateTime.Now, 20m, "Bought stationery", "Stationery")
        );
        expenseLedger.AddEntry(
            new ExpenseTransaction(3, DateTime.Now, 15m, "Team snacks", "Food")
        );

        decimal totalIncome = incomeLedger.CalculateTotal();
        decimal totalExpense = expenseLedger.CalculateTotal();

        Console.WriteLine("Total Income: $" + totalIncome);
        Console.WriteLine("Total Expenses: $" + totalExpense);
        Console.WriteLine("Net Balance: $" + (totalIncome - totalExpense));

        Console.WriteLine("\n--- Transaction Summary ---");
        List<Transaction> allTransactions = new List<Transaction>();

        List<IncomeTransaction> incomes = incomeLedger.GetAll();
        for (int i = 0; i < incomes.Count; i++)
        {
            allTransactions.Add(incomes[i]);
        }

        List<ExpenseTransaction> expenses = expenseLedger.GetAll();
        for (int i = 0; i < expenses.Count; i++)
        {
            allTransactions.Add(expenses[i]);
        }

        for (int i = 0; i < allTransactions.Count; i++)
        {
            Console.WriteLine(allTransactions[i].GetSummary());
        }
    }
}
