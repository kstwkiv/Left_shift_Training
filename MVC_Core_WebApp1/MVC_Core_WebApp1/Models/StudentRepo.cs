using MVC_Core_WebApp1.Models;
using System;
namespace MVC_Core_WebApp1.Models
{
    public class StudentRepo : IRepo<Student>
    {
        public static List<Student> studList = null;
        public StudentRepo()
        {
            if (studList == null)
            {
                //collection initializer
                studList = new List<Student>()
                {
					// object initializer
					new Student() { RollNo = 101, Name = "Alok", Address = "Pune", Age = 22 },
                    new Student() { RollNo = 102, Name = "Riya", Address = "Thane", Age = 20 },
                };
            }
        }
        // ----------------------------- ADDING DATA -----------------------------
        public bool AddData(Student obj)
        {
            bool flag = false;
            if (obj != null)
            {
                studList.Add(obj);
                flag = true;
            }
            else
            {
                throw new NullReferenceException("object is not initialize");
            }
            return flag;
        }
        // ----------------------------- DELETING DATA -----------------------------
        public bool DeleteData(int id)
        {
            bool flag = false;
            Student sObj = studList.Find(s => s.RollNo == id);
            if (sObj != null)
            {
                studList.Remove(sObj);
                flag = true;
            }
            else
            {
                throw new NullReferenceException("object is not initialize");
            }
            return flag;
        }
        // ----------------------------- SHOWING ALL DATA -----------------------------
        public List<Student> ShowAllData()
        {
            return studList;
        }
        public Student ShowDetailsByID(int id)
        {
            Student sObj = studList.Find(s => s.RollNo == id);
            return sObj;
        }
        public Student ShowDetailsByName(string name)
        {
            Student sObj = studList.Find(s => s.Name == name);
            return sObj;
        }
        // ----------------------------- UPDATING DATA -----------------------------
        public bool UpdateData(int id, Student obj)
        {
            bool flag = false;
            Student sObj = studList.Find(s => s.RollNo == id);
            if ((sObj!=null))
            {
                sObj.Name = obj.Name;
                sObj.Address = obj.Address;
                sObj.Age = obj.Age;
                flag = true;
            }
            return flag;
        }
    }
}