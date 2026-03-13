using System;

namespace MediSureClinic
{
    public class PatientBill
    {
        public string BillId { get; set; }
        public string PatientName { get; set; }
        public bool HasInsurance { get; set; }
        public decimal ConsultationFee { get; set; }
        public decimal LabCharges { get; set; }
        public decimal MedicineCharges { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FinalPayable { get; set; }


        public void CalculateBill()
        {
            GrossAmount=ConsultationFee+LabCharges+MedicineCharges;
            if (HasInsurance)
            {
                DiscountAmount=GrossAmount*0.10m;
            }
            else
            {
                DiscountAmount=0;
            }

            FinalPayable=GrossAmount-DiscountAmount;
        }
    }
    class Program
    {
        static PatientBill LastBill;
        static bool HasLastBill=false;
        static void Main(string[] args)
        {
            while (true)
            {
                ShowMenu();
                Console.Write("Enter your Option: ");
                String choice=Console.ReadLine();
                switch (choice)
                {
                    case "1":
                    CreateNewBill();
                    break;

                    case "2":
                    ViewLastBill();
                    break;
                    
                    case "3":
                    ClearLastBill();
                    break;

                    case "4":
                    Console.WriteLine("Thank You! Application cloased normally.");
                    return;

                    default:
                    Console.WriteLine("Invalid Option! Must choose between 1 and 4.");
                    break;

                }

            }
        }
        static void ShowMenu()
        {
            Console.WriteLine("\n================== MediSure Clinic Billing ==================");
            Console.WriteLine("1. Create New Bill (Enter Patient Details)");
            Console.WriteLine("2. View Last Bill");
            Console.WriteLine("3. Clear Last Bill");
            Console.WriteLine("4. Exit");
        }

        static void CreateNewBill()
        {
            PatientBill bill = new PatientBill();

            Console.Write("Enter Bill ID: ");
            bill.BillId = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(bill.BillId))
            {
                Console.WriteLine("Bill ID cannot be empty.");
                return;
            }

            Console.Write("Enter Patient Name: ");
            bill.PatientName = Console.ReadLine();

            Console.Write("Is patient insured? (Y/N): ");
            string insurance = Console.ReadLine();
            bill.HasInsurance = insurance.Equals("Y", StringComparison.OrdinalIgnoreCase);

            Console.Write("Enter Consultation Fee: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal consultFee) || consultFee <= 0)
            {
                Console.WriteLine("Consultation fee must be greater than zero.");
                return;
            }
            bill.ConsultationFee = consultFee;

            Console.Write("Enter Lab Charges: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal lab) || lab < 0)
            {
                Console.WriteLine("Lab charges cannot be negative.");
                return;
            }
            bill.LabCharges = lab;

            Console.Write("Enter Medicine Charges: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal med) || med < 0)
            {
                Console.WriteLine("Medicine charges cannot be negative.");
                return;
            }
            bill.MedicineCharges = med;

            bill.CalculateBill();
            LastBill = bill;

            Console.WriteLine("\nBill Created Successfully");
            Console.WriteLine("--------------------------------");
            Console.WriteLine($"Gross Amount     : {bill.GrossAmount:F2}");
            Console.WriteLine($"Discount Amount  : {bill.DiscountAmount:F2}");
            Console.WriteLine($"Final Payable    : {bill.FinalPayable:F2}");
        }

        static void ViewLastBill()
        {
            if (LastBill == null)
            {
                Console.WriteLine("No bill available.");
                return;
            }

            Console.WriteLine("\n----------- Last Bill -----------");
            Console.WriteLine($"Bill ID          : {LastBill.BillId}");
            Console.WriteLine($"Patient Name     : {LastBill.PatientName}");
            Console.WriteLine($"Insured          : {(LastBill.HasInsurance ? "Yes" : "No")}");
            Console.WriteLine($"Consultation Fee : {LastBill.ConsultationFee:F2}");
            Console.WriteLine($"Lab Charges      : {LastBill.LabCharges:F2}");
            Console.WriteLine($"Medicine Charges : {LastBill.MedicineCharges:F2}");
            Console.WriteLine($"Gross Amount     : {LastBill.GrossAmount:F2}");
            Console.WriteLine($"Discount Amount  : {LastBill.DiscountAmount:F2}");
            Console.WriteLine($"Final Payable    : {LastBill.FinalPayable:F2}");
            Console.WriteLine("--------------------------------");
        }
        static void ClearLastBill()
        {
            LastBill=null;
            HasLastBill=false;
            Console.WriteLine("Last bill cleared.");
        }
    }
}