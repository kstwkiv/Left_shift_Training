namespace ConstructorExample{
    class Program
{
    int i;
    bool b;
    static void Main(string[] args)
    {
        Program p=new ConstructorExample.Program();
        Console.WriteLine(p.i);
        Console.WriteLine(p.b);
        Console.ReadLine();
    }
}
}