using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ConArchDemo
{
    /// <summary>
    /// Demo for connected ARCHITECTURE in DAL class
    /// </summary>
    public class StudentDAL
    {
        SqlConnection conn = null;
        SqlCommand cmd = null;
        SqlDataReader sdr = null;

        public StudentDAL()
        {
            conn = new SqlConnection();
            conn.ConnectionString = "Server=.\\Sqlexpress;Integrated Security = SSPI;Database = LPU_DB;TrustServerCertificate=true";
        }
        public List<Student> ShowAllStudent()
        {
            List<Student> studList = null;
            //Code for Connected Architecture below
            try
            { 
                conn.Open();

                cmd = new SqlCommand();
                cmd.CommandText = "Select * from StudentInfo";
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                //holding data via reader(forward only control)
                sdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(sdr);

                //convert Table into List
                if(dt.Rows.Count > 0)
                {
                    studList = new List<Student>();
                }
                foreach (DataRow drow in dt.Rows)
                {
                    Student student = new Student()
                    {
                        RollNo = Convert.ToInt32(drow[0].ToString()),
                        Name = drow[1].ToString(),
                        Address = drow[3].ToString(),
                        PhoneNo = drow[5].ToString(),
                    };
                    if (student != null) {
                     studList.Add(student);
                    }

                }

                ///
                //older way ---------------------
                //foreach (DataRow row in sdr)
                //{
                //    dt.Rows.Add(row);
                //}
                ///

                //while (sdr.Read())
                //{
                //    Console.WriteLine();
                //}
            }
            catch (SqlExcption ex)
            {
                throw 
            }
            finally
            {
                conn.Close();
            }

            return studList;
        }

        public List<Student> ShowStudentbyName(string name)
        {
            List<Student> studList = null;

            return studList;
        }

        public Student ShowStudentbyID(int rollNo)
        {
            Student temObj = null;

            return temObj;
        }

    }
}