using System;
using System.Text.RegularExpressions;

public class Program
{
public static string validateTransaction(string record)
{
string pattern=@"^TXN-([1-9]\d{5})\|(\d{4}-\d{2}-\d{2})\|(USD|EUR|INR|GBP|AUD|CAD)\|((0|[1-9]\d{0,5})(\.\d{1,2})?)\|(SUCCESS|FAILED|PENDING)$";
Match m=Regex.Match(record,pattern);
if(!m.Success)return "INVALID LOG";

string id=m.Groups[1].Value;
if(Regex.IsMatch(id,@"(\d)\1\1\1"))return "INVALID LOG";

DateTime dt;
if(!DateTime.TryParse(m.Groups[2].Value,out dt))return "INVALID LOG";
if(dt.Year<2000||dt.Year>2099)return "INVALID LOG";

decimal amt=decimal.Parse(m.Groups[4].Value);
if(amt>999999.99m)return "INVALID LOG";

return "VALID LOG";
}

public static void Main(string[] args)
{
int N=int.Parse(Console.ReadLine());
for(int i=0;i<N;i++)
{
string record=Console.ReadLine();
Console.WriteLine(validateTransaction(record));
}
}
}