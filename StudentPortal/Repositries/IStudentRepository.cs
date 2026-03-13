using StudentPortal.Models;

namespace StudentPortal.Repositries
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllAsync(string q = null);
        Task<Student> GetByIdAsync(int id);
        Task AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(int id);
        Task<bool> StudentExistsAsync(int id);
    }
}
