using log4net;
using StudentManager.Common.Aggregates.StudentAggregate;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace StudentManager.DataAccess.SQL.Repositories
{
    public class SqlStudentRepository : IStudentRepository
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(SqlStudentRepository));
        private readonly string connectionString = "Server=.;Database=StudentManager;User Id=sa;Password=yourStrong(!)Password;";
        public IEnumerable<Student> GetAll()
        {
            List<Student> studentList;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                logger.Info("Connection is open");
                using (var command = new SqlCommand("select * from Student", connection))
                {
                    studentList = ReadDatabase(command);
                }
                connection.Close();
                logger.Info("Connection is close");
            }
            return studentList;
        }

        public Student Create(Student entity)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                logger.Info("Connection is open");
                var query = "INSERT INTO Student (Guid, Name, LastName, Birthday)";
                query += " VALUES (@Guid, @Name, @LastName, @Birthday)";

                using (var command = new SqlCommand(query, connection))
                {
                    GetSqlCommand(command, entity);
                    command.ExecuteNonQuery();
                    connection.Close();
                    logger.Info("Connection is close");
                }
            }
            return entity;
        }

        public Student Update(Student entity)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                logger.Info("Connection is open");
                var query = "UPDATE Student SET Name = @Name, LastName = @LastName, Birthday = @Birthday Where Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", entity.Id);
                    GetSqlCommand(command, entity);
                    command.ExecuteNonQuery();
                    connection.Close();
                    logger.Info("Connection is close");
                }
            }
            return entity;
        }

        public Student DeleteById(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                logger.Info("Connection is open");
                var query = "DELETE FROM Student WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                    connection.Close();
                    logger.Info("Connection is close");
                }
            }
            var student = GetAll();
            return student.FirstOrDefault(x => x.Id == id);
        }

        private List<Student> ReadDatabase(SqlCommand command)
        {
            var studentsList = new List<Student>();
            using (var reader = command.ExecuteReader())
            {
                
                if (reader.HasRows)
                {
                    studentsList = ReadStudents(reader);
                }
                else
                {
                    logger.Info("No data found");
                    Console.WriteLine("No data found.");
                }
            }
            
            return studentsList;
        }

        private List<Student> ReadStudents(SqlDataReader reader)
        {
            var studentList = new List<Student>();

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
            return studentList;
        }

        private void GetSqlCommand(SqlCommand command, Student entity)
        {
            command.Parameters.AddWithValue("@Guid", entity.Guid);
            command.Parameters.AddWithValue("@Name", entity.Name);
            command.Parameters.AddWithValue("@LastName", entity.LastName);
            command.Parameters.AddWithValue("@Birthday", entity.BirthDate);
        }


    }
}
