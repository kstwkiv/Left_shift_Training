namespace Cdemo{
    class ExConst{
    public ExConst()
        {
            Console.WriteLine("Cosntructor is called");
        }
    }
    class Program
{
    static void Main()
    {
        ExConst p=new ExConst();
        ExConst p1=new ExConst();
        ExConst p2=new ExConst();
    }
}
}