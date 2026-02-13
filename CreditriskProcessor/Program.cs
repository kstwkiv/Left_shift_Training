using System;
public class InvalidCreditDataException : Exception
{
    public InvalidCreditDataException(string message) : base(message)
    {
        
    }
}
public class CreditRiskProcessor
{
    public bool validateCustomerDetails(int age, String employmentType, double monthlyIncome, double dues, int creditScore, int defaults)
    {
        if(age <21|| age > 65)
        {
            throw new InvalidCreditDataException("Invalid Age");
        }

        if (employmentType != "Salaried" && employmentType != "Self-Employed")
        {
            throw new InvalidCreditDataException("Invalid Employment Type");
        }

        if (monthlyIncome < 20000)
        {
            throw new InvalidCreditDataException("Invalid Monthly Income");
        }

        if (dues < 0)
        {
            throw new InvalidCreditDataException("Invalid credit dues");
        }

        if(creditScore<300 && creditScore > 900)
        {
            throw new InvalidCreditDataException("Invalid credit score");
        }

        if (defaults < 0)
        {
            throw new InvalidCreditDataException("Invalid default count");
        }

        return true;
    }

    public double calculateCreditLimit(double monthlyIncome,double dues,int creditScore,int defaults)
    {
        double debtratio=dues/(monthlyIncome*12);
        if (creditScore < 600 || defaults >= 3 || debtratio > 0.4)
        {
            return 50000;
        }

        if ((creditScore >= 600 && creditScore <= 749) || (defaults == 1 || defaults == 2))
        {
            return 150000;
        }

        if (creditScore >= 750 && defaults == 0 && debtratio < 0.25)
        {
            return 300000;
        }

        return 50000;
    }
}
class program
{
    public static void Main(String[] args)
    {
        try
        {
            CreditRiskProcessor processor=new CreditRiskProcessor();
            Console.WriteLine("Enter Cust name: ");
            string name=Console.ReadLine();
            Console.Write("Enter age: ");
            int age = int.Parse(Console.ReadLine());
            Console.Write("Enter employment type: ");
            string employment = Console.ReadLine();
            Console.Write("Enter monthly income: ");
            double income = double.Parse(Console.ReadLine());

            Console.Write("Enter existing credit dues: ");
            double dues = double.Parse(Console.ReadLine());
            Console.Write("Enter credit score: ");
            int score = int.Parse(Console.ReadLine());
            Console.Write("Enter number of loan defaults: ");
            int defaults = int.Parse(Console.ReadLine());

            if (processor.validateCustomerDetails(age, employment, income, dues, score, defaults))
            {
                double limit=processor.calculateCreditLimit(income,dues,score,defaults);
                Console.WriteLine($"cust name : {name}");
                Console.WriteLine($"approved credit limit: Rs {limit}");
            }
        }
        catch(InvalidCreditDataException e)
        {
            Console.WriteLine(e);
        }
    }
}