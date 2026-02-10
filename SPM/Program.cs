class SPM
{
    private int sid;
    private string name;
    private int age;
    private int marks;
    private string password;
    //below is a normal property with validation
    public string Name
    {
        get{return name;}
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                name=value;
            }
        }
    }
    public int Marks
    {
        get{return marks;}
        set{if(value>=0 && value<=100){marks=value;}}
    }
    // below is a auto implemented property
    public int SID{get; set;}
    //below is a read only property
    public string Results
    {
        get{return marks>=40?"Pass":"Fail";}
    }
    public int Age
    {
        get{return age;}
        set{if(value>0){age =value;}}
    }
    //below is a writeonly property
    public string Password
    {
        set{if(value.Length>=6){password=value;}}
    }
}
class Program
{
    static void Main()
    {
        SPM s=new SPM();
        s.Name="women";
        s.Age=22;
        s.SID=12221254;
        s.Marks=69;
        s.Password="sweety@2";

        Console.WriteLine("Name: "+s.Name);
        Console.WriteLine("Age: "+s.Age);
        Console.WriteLine("marks: "+s.Marks);
        Console.WriteLine("Results: "+s.Results);
        Console.WriteLine("ID: "+s.SID);
        //Console.WriteLine("Password: "+s.Password);
    }
}