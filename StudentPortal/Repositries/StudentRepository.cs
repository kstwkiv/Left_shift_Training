using Microsoft.EntityFrameworkCore;
using StudentPortal.Models;

namespace StudentPortal.Repositries
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentPortalDbContext _db;

        public StudentRepository(StudentPortalDbContext db)
        {
            _db = db; 
        }

        public async Task<List<Student>> GetAllAsync(string q = null)
        {
            var query = _db.Students.AsQueryable();

            if (!string.IsNullOrEmpty(q))
            {
                q = q.Trim().ToLower();
                query = query.Where(s => s.FullName.ToLower().Contains(q) || s.Email.ToLower().Contains(q));
            }

            return await query.AsNoTracking().OrderByDescending(s => s.CreatedAt).ToListAsync();
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return await _db.Students.FirstOrDefaultAsync(m => m.StudentId == id);
        }

        public async Task AddAsync(Student student)
        {
            _db.Add(student);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Student student)
        {
            _db.Update(student);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var student = await _db.Students.FindAsync(id);
            if (student != null)
            {
                _db.Students.Remove(student);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<bool> StudentExistsAsync(int id)
        {
            return await _db.Students.AnyAsync(e => e.StudentId == id);
        }
    }
}
