class StudentProfile
{
    private string name;
    private int age;
    private int marks;

    //properties
    //A property is a method in disguise
    public string Name
    {
        get{return name;}
        set{ if(!string.IsNullOrEmpty(value)){name=value;}}
    }
    public int Age
    {
        get{return age;}
        set
        {
            if (value > 0)
            {
                age=value;
            }
        }
    }
    public int Marks
    {
        get{return marks;}
        set{if(value>=0 && value <= 100)
            {
                marks=value;
            }
        }
    }

}
class Program
{
    static void Main()
    {
        StudentProfile s=new StudentProfile();
        s.Name="Aman";
        s.Age=21;
        s.Marks=32;
        Console.WriteLine("Name: "+s.Name);
        Console.WriteLine("Age: "+s.Age);
        Console.WriteLine("marks: "+s.Marks);
    }
}