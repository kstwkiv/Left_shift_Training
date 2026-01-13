using System;

namespace PhoneSubscriptionWithDelegates
{
    public delegate void CallEventHandler(object sender, CallEventArgs e);

    public class CallEventArgs : EventArgs
    {
        public string PhoneNumber { get; set; }
        public decimal Minutes { get; set; }
        public decimal Balance { get; set; }
    }

    public class PhoneSubscriptionService
    {
        private decimal balance = 100.0m;
        public string PhoneNumber { get; set; } = "+91-XXXXXXXXXX";

        public CallEventHandler CallMade;
        public CallEventHandler LowBalance;

        public void MakeCall(decimal minutes)
        {
            decimal cost = minutes * 0.5m;
            
            if (balance < cost)
            {
                if (LowBalance != null)
                    LowBalance(this, new CallEventArgs 
                    { PhoneNumber = PhoneNumber, Balance = balance });
                Console.WriteLine("Low balance!");
                return;
            }

            balance -= cost;
            
            if (CallMade != null)
                CallMade(this, new CallEventArgs 
                { PhoneNumber = PhoneNumber, Minutes = minutes, Balance = balance });
            
            Console.WriteLine($"{minutes}min call, Balance: ₹{balance:F2}");
        }
    }

    class Program
    {
        static void Main()
        {
            var service = new PhoneSubscriptionService();
            
            service.CallMade = new CallEventHandler(OnCallMade);
            service.LowBalance = new CallEventHandler(OnLowBalance);

            service.MakeCall(30);  
            service.MakeCall(180); 
        }

        static void OnCallMade(object sender, CallEventArgs e)
        {
            Console.WriteLine($"{e.PhoneNumber}: {e.Minutes}min OK");
        }

        static void OnLowBalance(object sender, CallEventArgs e)
        {
            Console.WriteLine($"{e.PhoneNumber}: ₹{e.Balance:F2} left!");
        }
    }
}
