public class Book
{
    public string Id{get;set;}
    public string Title{get;set;}
    public int Price{get;set;}
    public int Stock{get;set;}
    public Book(string id,string title,int price,int Stock)
    {
        Id=id;
        Title=title;
        Price=price;
        this.Stock=Stock;
    }
}
public class BookUtility
{
    private Book book;

    public BookUtility(Book book)
    {
        this.book=book;
    }
    public void BookDetails()
    {
        Console.WriteLine($"Details: {book.Id} {book.Title} {book.Price} {book.Stock}");
    }
    public void UpdatedBookPrice(int newprice)
    {
        book.Price=newprice;
        Console.WriteLine($"Updated Price: {newprice}");
    }

    public void UpdateBookStock(int newstock)
    {
        book.Stock=newstock;
        Console.WriteLine($"Updated Stocks: {newstock}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        string[] input=Console.ReadLine().Split(' ');
        string id=input[0];
        string title=input[1];
        int price=int.Parse(input[2]);
        int stock=int.Parse(input[3]);
        Book book=new Book(id,title,price,stock);
        BookUtility utility=new BookUtility(book);
        while (true)
        {
            int choice=int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                utility.BookDetails();
                break;
                case 2:
                int newprice=int.Parse(Console.ReadLine());
                utility.UpdatedBookPrice(newprice);
                break;
                case 3:
                int stocks=int.Parse(Console.ReadLine());
                utility.UpdateBookStock(stocks);
                break;
                case 4:
                Console.WriteLine("Thank You");
                return;

                default:
                break;
            }
        }
    }
}