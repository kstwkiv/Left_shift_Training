public class InvalidPriceException:Exception
{
    public InvalidPriceException(string Message):base(Message){

    }
}
public class DuplicateMedicineException : Exception
{
    public DuplicateMedicineException(string Message) : base(Message)
    {
        
    }
}
public class InvalidExpiryYearException : Exception
{
    public InvalidExpiryYearException(string Message):base(Message)
    {
        
    }
}
public class MedicineNotFoundException : Exception
{
    public MedicineNotFoundException(string Message) : base(Message)
    {
        
    }
}
public class Medicine
{
    public string Id{get;set;}
    public string Name{get;set;}
    public int Price{get;set;}
    public int ExpiryYear{get;set;}
    public Medicine(string id,string name,int price,int year)
    {
        Id=id;
        Name=name;
        Price=price;
        ExpiryYear=year;
    }
}
public class MedicineUtility{
        private SortedDictionary<int,List<Medicine>> medicines=new SortedDictionary<int, List<Medicine>>();
        public void AddMedicine(Medicine medicine)
    {
        if (medicine.Price <= 0)
        {
            throw new InvalidPriceException($"Price must be greater than 0");
        }

        if (medicine.ExpiryYear < DateTime.Now.Year)
        {
            throw new InvalidExpiryYearException($"Expiry Year cannot be in the past");
        }

        if (!medicines.ContainsKey(medicine.ExpiryYear))
        {
            medicines[medicine.ExpiryYear]=new List<Medicine>();
        }

        foreach(var med in medicines.Values)
        {
            foreach(var item in med)
            {
                if (item.Id == medicine.Id)
                {
                    throw new DuplicateMedicineException($"Product already exixts");
                }
            }
        }
        medicines[medicine.ExpiryYear].Add(medicine);

    }
    public void GetAllMedicines()
    {
        if (medicines.Count == 0)
        {
            Console.WriteLine($"No medicines available");
            return;
        }

        foreach(var year in medicines.Keys)
        {
            foreach(var med in medicines[year])
            {
                Console.WriteLine($"Details : {med.Id} {med.Name} {med.Price} {med.ExpiryYear}");
            }
        }
    }
    public void UpdateMedicinePrice(string id,int newprice)
    {
        if (newprice <= 0)
        {
            throw new InvalidPriceException($"Price must be greater than 0");
        }

        foreach(var list in medicines.Values)
        {
            foreach(var med in list)
            {
                if (med.Id == id)
                {
                    med.Price=newprice;
                    return ;
                }
            }
        }

        throw new MedicineNotFoundException("medicine not found");
    }
}
class Program
{
    static void Main(string[] args)
    {
        MedicineUtility util=new MedicineUtility();
        while (true)
        {
            Console.WriteLine("1 Display all medicines");
            Console.WriteLine("2 update medicine price");
            Console.WriteLine("3 add medicine");
            Console.WriteLine("4 Exit");
            int choice=int.Parse(Console.ReadLine());
            try
            {
                switch (choice)
                {
                    case 1:{
                    util.GetAllMedicines();
                    break;}
                    case 2:
                    {Console.WriteLine("enter id");
                    string id=Console.ReadLine();

                    Console.WriteLine("enter new price");
                    int newprice=int.Parse(Console.ReadLine());

                    util.UpdateMedicinePrice(id,newprice);
                    Console.WriteLine("Price updated successfully");
                    break;}
                    case 3:
                    {Console.WriteLine("Eneter med id name price expiry-year");
                    string[] input=Console.ReadLine().Split(' ');
                    string id=input[0];
                    string name=input[1];
                    int price=int.Parse(input[2]);
                    int expiryyear=int.Parse(input[3]);
                    Medicine med=new Medicine(id,name,price,expiryyear);
                    util.AddMedicine(med);
                    break;}
                    case 4:
                    return;
                    default:
                    Console.WriteLine("Invalid choice.");
                    break;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}