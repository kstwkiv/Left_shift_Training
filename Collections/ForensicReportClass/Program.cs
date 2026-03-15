using System;
using System.Collections.Generic;
using System.Globalization;
public class ForensicReport
{
    Dictionary<string,DateTime> reportMap=new Dictionary<string, DateTime>();

    public void addReportDetails()
    {
        Console.WriteLine("Enter the name of the Reporting Office:");
        string reportingOfficeName=Console.ReadLine();
        Console.WriteLine("Enter the date on which the report is filed:");
        DateTime reportFiledDate=DateTime.ParseExact(Console.ReadLine(),"yyyy-MM-dd",CultureInfo.InvariantCulture);
        reportMap.Add(reportingOfficeName,reportFiledDate);
    }

    public List<string>getOfficersWhoFilesReportsOnDate(DateTime reportFiledDate)
    {
        List<string> officers=new List<string>();
        foreach(var item in reportMap)
        {
            if (item.Value.Date == reportFiledDate.Date)
            {
                officers.Add(item.Value);
            }
        }

        return officers;
    }
}
class Program
{
    public static void Main(String[] args)
    {
        Console.WriteLine("Enter the num of reports: ");
        int n=int.Parse(Console.ReadLine());
        ForensicReport report=new ForensicReport();
        for(int i = 0; i < n; i++)
        {
            report.addReportDetails();
        }
        Console.WriteLine("Enter the filed Date to identify the reporting officers");
        DateTime searchDate=DateTime.ParseExact(Console.ReadLine(),"yyyy-MM-dd",CultureInfo.InvariantCulture);

        List<string> results=report.getOfficersReportsOnDate(searchDate);
        if (results.Count== 0)
        {
            Console.WriteLine("No reporting officer filed the report");
        }
        else
        {
            Console.WriteLine("reports filed on the "+searchDate.ToString("yyyy-MM-dd")+" are by");
            foreach(string name in results)
        {

            Console.WriteLine(name);
        }
        }
    }
}