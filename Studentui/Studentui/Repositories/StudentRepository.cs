using Studentui.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace Studentui.Repositories
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetAll();
        Student? GetById(int id);
        int Create(Student student);
        void Update(Student student);
        void Delete(int id);
    }

    public class StudentRepository : IStudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(string connectionString)
        {
            _connectionString = connectionString
                ?? throw new ArgumentNullException(nameof(connectionString));
        }

        // ----------------------------------------------------------------
        // GET ALL
        // ----------------------------------------------------------------
        public IEnumerable<Student> GetAll()
        {
            var students = new List<Student>();

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_GetAllStudents", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                students.Add(MapStudent(reader));

            return students;
        }

        // ----------------------------------------------------------------
        // GET BY ID
        // ----------------------------------------------------------------
        public Student? GetById(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_GetStudentById", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@StudentId", id);

            conn.Open();
            using var reader = cmd.ExecuteReader();
            return reader.Read() ? MapStudent(reader) : null;
        }

        // ----------------------------------------------------------------
        // CREATE
        // ----------------------------------------------------------------
        public int Create(Student s)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_CreateStudent", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@FirstName", s.FirstName);
            cmd.Parameters.AddWithValue("@LastName", s.LastName);
            cmd.Parameters.AddWithValue("@Email", s.Email);
            cmd.Parameters.AddWithValue("@Phone", (object?)s.Phone ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DateOfBirth", (object?)s.DateOfBirth ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Course", s.Course);
            cmd.Parameters.AddWithValue("@EnrollDate", s.EnrollDate);

            conn.Open();
            var result = cmd.ExecuteScalar();
            return Convert.ToInt32(result);
        }

        // ----------------------------------------------------------------
        // UPDATE
        // ----------------------------------------------------------------
        public void Update(Student s)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_UpdateStudent", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@StudentId", s.StudentId);
            cmd.Parameters.AddWithValue("@FirstName", s.FirstName);
            cmd.Parameters.AddWithValue("@LastName", s.LastName);
            cmd.Parameters.AddWithValue("@Email", s.Email);
            cmd.Parameters.AddWithValue("@Phone", (object?)s.Phone ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DateOfBirth", (object?)s.DateOfBirth ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Course", s.Course);
            cmd.Parameters.AddWithValue("@EnrollDate", s.EnrollDate);
            cmd.Parameters.AddWithValue("@IsActive", s.IsActive);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        // ----------------------------------------------------------------
        // DELETE
        // ----------------------------------------------------------------
        public void Delete(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_DeleteStudent", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@StudentId", id);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        // ----------------------------------------------------------------
        // PRIVATE MAPPER
        // ----------------------------------------------------------------
        private static Student MapStudent(SqlDataReader r) => new()
        {
            StudentId = r.GetInt32(r.GetOrdinal("StudentId")),
            FirstName = r.GetString(r.GetOrdinal("FirstName")),
            LastName = r.GetString(r.GetOrdinal("LastName")),
            Email = r.GetString(r.GetOrdinal("Email")),
            Phone = r.IsDBNull(r.GetOrdinal("Phone")) ? null : r.GetString(r.GetOrdinal("Phone")),
            DateOfBirth = r.IsDBNull(r.GetOrdinal("DateOfBirth")) ? null : r.GetDateTime(r.GetOrdinal("DateOfBirth")),
            Course = r.GetString(r.GetOrdinal("Course")),
            EnrollDate = r.GetDateTime(r.GetOrdinal("EnrollDate")),
            IsActive = r.GetBoolean(r.GetOrdinal("IsActive"))
        };
    }
}
