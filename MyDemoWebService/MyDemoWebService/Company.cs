using System;
using System.Data;
using System.Data.SqlClient;

namespace MyDemoWebService
{
    public class Company
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }

        public Company[] ShowAllDetails()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=;Integrated Security=true;Database=Lpu";
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from Company";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            SqlDataReader reader = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(reader);

            Company[] compList = null;

            if (dt.Rows.Count > 0)
            {
                compList = new Company[dt.Rows.Count];
            }

            int count = 0;

            foreach (DataRow item in dt.Rows)
            {
                Company cobj = new Company();

                cobj.id = (int)item[0];
                cobj.Name = item[1].ToString();
                cobj.Salary = int.Parse(item[2].ToString());

                compList[count] = cobj;
                count++;
            }

            conn.Close();
            return compList;
        }
    }
}