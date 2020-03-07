using StudentManager.Common.Aggregates.StudentAggregate;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace StudentManager.DataAccess.SQL.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string connectionString = "Server=.;Database=students;User Id=sa;Password=yourStrong(!)Password;";
        public IEnumerable<Student> GetAll()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("select * from student", connection))
                {
                    var reader = command.ExecuteReader();
                }
            }
            return null;
        }

        public Student Create(Student entity)
        {
            throw new NotImplementedException();
        }

        public Student Update(Student entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
