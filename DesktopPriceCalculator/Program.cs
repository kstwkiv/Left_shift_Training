using System;

public class Computer
{
    public string Processor { get; set; }
    public int RamSize { get; set; }
    public int HardDiskSize { get; set; }
    public int GraphicCard { get; set; }
}

public class Desktop : Computer
{
    public int MonitorSize { get; set; }
    public int PowerSupplyVolt { get; set; }

    public double DesktopPriceCalculation()
    {
        int ProcessorCost = 0;
        if (Processor == "i3")
        {
            ProcessorCost = 1500;
        }
        else if (Processor == "i5")
        {
            ProcessorCost = 3000;  // Fixed: was P=3000
        }
        else if (Processor == "i7")
        {
            ProcessorCost = 4500;
        }

        int RamPricePerGB = 200;
        int HardDiskPerTB = 1500;
        int GraphicCardPerGB = 2500;
        int PowerSupplyPerVolt = 20;
        int MonitorPerInch = 250;

        double price = 0;
        price += ProcessorCost;
        price += RamSize * RamPricePerGB;
        price += HardDiskSize * HardDiskPerTB;  // Fixed: was Price+=
        price += GraphicCard * GraphicCardPerGB;
        price += MonitorSize * MonitorPerInch;
        price += PowerSupplyVolt * PowerSupplyPerVolt;

        return price;
    }
}

public class Laptop : Computer
{
    public int DisplaySize { get; set; }
    public int BatteryVolt { get; set; }

    public double LaptopPriceCalculation()
    {
        int ProcessorCost = 0;  // Fixed: added missing semicolon

        if (Processor == "i3")
        {
            ProcessorCost = 2500;
        }
        else if (Processor == "i5")
        {
            ProcessorCost = 5000;
        }
        else if (Processor == "i7")
        {
            ProcessorCost = 6500;
        }

        int RamPricePerGB = 200;
        int HardDiskPerTB = 1500;
        int GraphicCardPerGB = 2500;
        int BatteryPerVolt = 20;
        int DisplayPerInch = 250;

        double price = ProcessorCost + (RamSize * RamPricePerGB) + (HardDiskSize * HardDiskPerTB) + 
                      (GraphicCard * GraphicCardPerGB) + (DisplaySize * DisplayPerInch) + (BatteryVolt * BatteryPerVolt);
        return price;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("1.Desktop");
        Console.WriteLine("2.Laptop");
        Console.WriteLine("Choose the option");
        int opt = int.Parse(Console.ReadLine());

        if (opt == 1)
        {
            Desktop desktop = new Desktop();
            Console.WriteLine("Enter the processor");
            desktop.Processor = Console.ReadLine();
            Console.WriteLine("Enter the ram size");
            desktop.RamSize = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the hard disk size");
            desktop.HardDiskSize = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the graphic card size");  // Added missing input
            desktop.GraphicCard = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the monitor size");
            desktop.MonitorSize = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the power supply volt");
            desktop.PowerSupplyVolt = int.Parse(Console.ReadLine());

            double price = desktop.DesktopPriceCalculation();
            Console.WriteLine("Desktop price is " + price);
        }
        else if (opt == 2)
        {
            Laptop laptop = new Laptop();  // Fixed: was desktop
            Console.WriteLine("Enter the processor");
            laptop.Processor = Console.ReadLine();  // Fixed: was desktop
            Console.WriteLine("Enter the ram size");
            laptop.RamSize = int.Parse(Console.ReadLine());  // Fixed: was desktop
            Console.WriteLine("Enter the hard disk size");
            laptop.HardDiskSize = int.Parse(Console.ReadLine());  // Fixed: was desktop
            Console.WriteLine("Enter the graphic card size");  // Added missing input
            laptop.GraphicCard = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the display size");
            laptop.DisplaySize = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the battery volt");
            laptop.BatteryVolt = int.Parse(Console.ReadLine());  // Fixed: was desktop

            double price = laptop.LaptopPriceCalculation();
            Console.WriteLine("Laptop price is " + price);
        }
    }
}
