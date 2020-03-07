using StudentManager.Common.Aggregates.StudentAggregate;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace StudentManager.DataAccess.SQL.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string connectionString = "Server=.;Database=StudentManager;User Id=sa;Password=yourStrong(!)Password;";
        public IEnumerable<Student> GetAll()
        {
            var studentList = new List<Student>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("select * from Student", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        
                        while (reader.Read())
                        {
                            var student = new Student
                            {
                                Id = int.Parse(reader["Id"].ToString()),
                                Guid = Guid.Parse(reader["Guid"].ToString()),
                                Name = reader["Name"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                BirthDate = DateTime.Parse(reader["Birthday"].ToString())
                            };
                            studentList.Add(student);
                        }

                        connection.Close();
                    }
                }
            }
            return studentList;
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
