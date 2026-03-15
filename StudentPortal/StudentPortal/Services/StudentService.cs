using Microsoft.EntityFrameworkCore;
using StudentPortal.Models;
using StudentPortal.Repositries;

namespace StudentPortal.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;

        public StudentService(IStudentRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Student>> SearchAsync(string q = null) => await _repo.GetAllAsync(q);

        public async Task<List<Student>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<Student> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task AddAsync(Student student) => await _repo.AddAsync(student);

        public async Task UpdateAsync(Student student) => await _repo.UpdateAsync(student);

        public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);

        public async Task<bool> StudentExistsAsync(int id) => await _repo.StudentExistsAsync(id);
    }
}
