
using System.Xml.Linq;

namespace WebAPIDemo.Model.Repo
{
    public class StudentRepo : IRepo<Student>
    {
        public static List<Student> studList = null;
        public StudentRepo(){
            if (studList == null)
            {
                studList = new List<Student>()
                {
                    new Student() { RollNo = 101,Name = "Sathwika",city = "Jalandhar",PhoneNo = "9573171390"},
                    new Student() { RollNo = 102,Name = "samindra",city = "Mumbai",PhoneNo = "9963309253"},
                    new Student() { RollNo = 103,Name = "Rohit",city = "Vadodara",PhoneNo = "9736282634"}
                };
            }
        }
        public bool Add(Student item)
            {

            bool flag = false;
            Student stu = studList.Find(s => s.RollNo ==item.RollNo);
            if (stu==null)
            {
                studList.Add(item);
                flag = true;
            }

            return flag;
          }

        public bool Delete(int id)
        {
            bool flag = false;
            Student estu = studList.Find(s => s.RollNo == id);
            if (estu != null)
            {
                studList.Remove(estu);
                flag = true;
            }
            return flag;
        }

        public Student Get(int id)
        {
            Student stu = studList.Find(s => s.RollNo == id);
            if (stu != null)
            {
                return stu;
            }
            else
            {
                throw new Exception("Student record not found");
            }
        }

        public ICollection<Student> GetAll()
        {
            return studList;
        }

        public bool Update(int id, Student cstu)
        {
            bool flag = false;
            Student estu = studList.Find(s => s.RollNo == id);
            if (estu!=null && cstu!=null)
            {
                estu.Name=cstu.Name;
                estu.city = cstu.city;
                estu.PhoneNo = cstu.PhoneNo;
                flag = true;
            }
            return flag;
        }
    };
}
