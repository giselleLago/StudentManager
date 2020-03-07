using StudentManager.Common.Aggregates.StudentAggregate;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

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
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "INSERT INTO Student (Id, Guid, Name, LastName, Birthday)";
                query += " VALUES (@Id, @Guid, @Name, @LastName, @Birthday)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", entity.Id);
                    command.Parameters.AddWithValue("@Guid", entity.Guid);
                    command.Parameters.AddWithValue("@Name", entity.Name);
                    command.Parameters.AddWithValue("@LastName", entity.LastName);
                    command.Parameters.AddWithValue("@Birthday", entity.BirthDate);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return entity;
        }

        public Student Update(Student entity)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "UPDATE Student SET Name = @Name, LastName = @LastName, Birthday = @Birthday Where Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", entity.Id);
                    command.Parameters.AddWithValue("@Guid", entity.Guid);
                    command.Parameters.AddWithValue("@Name", entity.Name);
                    command.Parameters.AddWithValue("@LastName", entity.LastName);
                    command.Parameters.AddWithValue("@Birthday", entity.BirthDate);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return entity;
        }

        public void DeleteById(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "DELETE FROM Student WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            
        }
    }
}
