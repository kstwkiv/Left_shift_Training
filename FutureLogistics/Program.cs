using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Text;
public abstract class GoodsTransport
{
    //abstract->protected attributes
    protected string transportID;
    protected string transportDate;
    protected int transportRating;

    //public attributes-> constructor
    public GoodsTransport(string transportID,string transportDate,int transportRating)
    {
        this.transportID=transportID;
        this.transportDate=transportDate;
        this.transportRating=transportRating;
    }
    //getter setter methods
    public string TransportID
    {
        get {return transportID;}
        set {transportID=value;}
    }
    public string TransportDate
    {
        get {return transportDate;}
        set {transportDate=value;}
    }
    public int TransportRating
    {
        get {return transportRating;}
        set {transportRating=value;}
    }
    //abstract methods
    public abstract string vehicleSelection();
    public abstract float calculateTotalCharge();
}
class BrickTransport : GoodsTransport
{
    private float brickSize;
    private float brickPrice;
    private int brickQuantity;
    public float BrickSize
    {
        get{return brickSize;}
        set{brickSize=value;}
    }
    public int BrickQuantity
    {
        get{return brickQuantity;}
        set{brickQuantity=value;}
    }
    public float BrickPrice{
        get{return brickPrice;}
        set{brickPrice=value;}
    }
    public BrickTransport(string transportID,string transportDate,int transportRating,float brickSize,int brickQuantity,float brickPrice):base(transportID,transportDate,transportRating)
    {
        this.brickPrice=brickPrice;
        this.brickQuantity=brickQuantity;
        this.brickSize=brickSize;
    }
    public override string vehicleSelection()
    {
        if (brickQuantity < 300)
        {
            return "Truck";
        }
        else if (brickQuantity >= 300 && brickQuantity<=500)
        {
            return "Lorry";
        }
        else
        {
            return "MonsterLorry";
        }
    }
    public override float calculateTotalCharge()
    {
        float totalBrickCost=brickPrice*brickQuantity;
        string vehicle=vehicleSelection();
        float vehiclePrice=0;
        if (vehicle.Equals("Truck", StringComparison.OrdinalIgnoreCase))
        {
            vehiclePrice=1000;
        }
        else if (vehicle.Equals("Lorry", StringComparison.OrdinalIgnoreCase))
        {
            vehiclePrice=1700;
        }
        else
        {
            vehiclePrice=3000;
        }

        float tax=totalBrickCost*0.30f;
        float discount=totalBrickCost*(transportRating/100.0f);
        float totalCharge=totalBrickCost+vehiclePrice+tax-discount;

        return totalCharge;
    }
}

class TimberTransport : GoodsTransport
{
    private float timberLength;
    private float timberRadius;
    private string timberType;
    private float timberPrice;
    public float TimberLength
    {
        get{return timberLength;}
        set {timberLength=value;}
    }
    public float TimberRadius
    {
        get{return timberRadius;}
        set {timberRadius=value;}
    }
    public string TimberType
    {
        get{return timberType;}
        set{timberType=value;}
    }
    public float TimberPrice
    {
        get{return timberPrice;}
        set{timberPrice=value;}
    }
    public TimberTransport(string transportID,string transportDate,int transportRating,float timberLength,float timberRadius,string timberType,float timberPrice):base(transportID,transportDate,transportRating){
        this.timberLength=timberLength;
        this.timberPrice=timberPrice;
        this.timberRadius=timberRadius;
        this.timberType=timberType;
    }
    public override string vehicleSelection()
    {
        float timberArea= 2 * 3.147f * timberRadius * timberLength;
        if (timberArea < 250)
        {
            return "Truck";
        }
        else if(timberArea>=250 && timberArea <= 400)
        {
            return "Lorry";
        }
        else
        {
            return "MonsterLorry";
        }
    }
    public override float calculateTotalCharge()
    {
        float volume=3.147f* timberRadius * timberRadius * timberLength;
        float rate=0;
        if (timberType.Equals("Premium", StringComparison.OrdinalIgnoreCase))
        {
            rate=0.20f;
        }
        else
        {
            rate=0.15f;
        }

        float price=volume * timberPrice * rate;
        string vehicle=vehicleSelection();
        float vprice=0;
        if (vehicle.Equals("Truck", StringComparison.OrdinalIgnoreCase))
        {
            vprice=1000;
        }else if (vehicle.Equals("Lorry", StringComparison.OrdinalIgnoreCase))
        {
            vprice=1700;
        }
        else
        {
            vprice=3000;
        }

        float tax=0.30f*price;
        float discount=price*(transportRating/100f);
        float totalcharge=price+vprice+tax-discount;

        return totalcharge;
    }
}
public class Utility
{
    public bool validateTransportId(string transportID)
    {
        string pattern=@"^RTS[0-9]{3}[A-Z]$";
        if (!Regex.IsMatch(transportID, pattern))
        {
            Console.WriteLine($"transport Id {transportID} is inavlid.");
            Console.WriteLine($"Pleave enter a valid transport id");
            return false;
        }

        return true;
    }
    public GoodsTransport parseDetails(String input)
    {
        string[] data=input.Split(':');
        string id=data[0];
        string date=data[1];
        int rating =int.Parse(data[2]);
        string type=data[3];
        if (!validateTransportId(id))
        {
            return null;
        }

        if (type.Equals("BrickTransport", StringComparison.OrdinalIgnoreCase))
        {
            float size=float.Parse(data[4]);
            int quantity=int.Parse(data[5]);
            float price=float.Parse(data[6]);

            return new BrickTransport(id,date,rating,size,quantity,price);
        }
        else
        {
            float length=float.Parse(data[4]);
            float radius=float.Parse(data[5]);
            string timberType=data[6];
            float price =float.Parse(data[7]);

            return new TimberTransport(id,date,rating,length,radius,timberType,price);
        }
    }
    public String findObjectType(GoodsTransport goodsTransport)
    {
        if(goodsTransport is TimberTransport)
        {
            return "TimberTransport";
        }
        else
        {
            return "BrickTransport";
        }

    }
}
class Program
{
    public static void Main(String[] args)
    {
        Utility util=new Utility();
        Console.WriteLine("Enter the Goods TRansport details");
        string input=Console.ReadLine();
        GoodsTransport obj1=util.parseDetails(input);
        if (obj1 == null)
        {
            return;
        }

        string type=util.findObjectType(obj1);
        Console.WriteLine($"Transporter id : {obj1.TransportID}");
        Console.WriteLine($"Date of transport : {obj1.TransportDate}");
        Console.WriteLine($"Rating of the transport : {obj1.TransportRating}");

        if (type == "BrickTransport")
        {
            BrickTransport b = (BrickTransport)obj1;
            Console.WriteLine($"Quantity of bricks : {b.BrickQuantity}");
            Console.WriteLine($"Brick price : {b.BrickPrice}");
        }
        else
        {
            TimberTransport t = (TimberTransport)obj1;
            Console.WriteLine($"Type of the timber : {t.TimberType}");
            Console.WriteLine($"Timber price per kilo : {t.TimberPrice}");
        }

        Console.WriteLine($"Vehicle for transport : {obj1.vehicleSelection()}");
        Console.WriteLine($"Total charge : {obj1.calculateTotalCharge()}");
    }
}