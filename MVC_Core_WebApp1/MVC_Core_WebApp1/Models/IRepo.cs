namespace MVC_Core_WebApp1.Models
{
    public interface IRepo<T>
    {
        bool AddData(T obj);
        bool UpdateData(int id, T obj);
        bool DeleteData(int id);

        List<T> ShowAllData();

        T ShowDetailsByID(int id);

    }
}