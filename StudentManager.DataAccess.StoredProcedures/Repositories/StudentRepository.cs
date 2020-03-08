using StudentManager.Common.Aggregates.StudentAggregate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace StudentManager.DataAccess.StoredProcedures.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string connectionString = "Server=.;Database=StudentManager;User Id=sa;Password=yourStrong(!)Password;";
        public IEnumerable<Student> GetAll()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string procedure = @"dbo.[GetAllStudents]";
                SqlCommand command = new SqlCommand(procedure, connection);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                var studentsList = ReadDatabase(command);

                connection.Close();

                return studentsList;

            }
        }

        public Student Create(Student entity)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string procedure = @"dbo.[AddStudent]";
                SqlCommand command = new SqlCommand(procedure, connection);
                command.Parameters.AddWithValue("@student_name", entity.Name);
                command.Parameters.AddWithValue("@student_surname", entity.LastName);
                command.Parameters.AddWithValue("@student_birth_date", entity.BirthDate);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();

                command.ExecuteNonQuery();

                connection.Close();
            }
            return entity;
        }

        public Student Update(Student entity)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string procedure = @"dbo.[UpdateStudent]";
                SqlCommand command = new SqlCommand(procedure, connection);
                command.Parameters.AddWithValue("@student_id", entity.Id);
                command.Parameters.AddWithValue("@student_name", entity.Name);
                command.Parameters.AddWithValue("@student_surname", entity.LastName);
                command.Parameters.AddWithValue("@student_birth_date", entity.BirthDate);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();

                command.ExecuteNonQuery();

                connection.Close();
            }
            return entity;
        }

        public void DeleteById(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string procedure = @"dbo.[DeleteStudent]";
                SqlCommand command = new SqlCommand(procedure, connection);
                command.Parameters.AddWithValue("@student_id", id);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();

                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        private List<Student> ReadDatabase(SqlCommand command)
        {
            var reader = command.ExecuteReader();
            var studentsList = new List<Student>();
            if (reader.HasRows)
            {
                studentsList = ReadStudents(reader);
            }
            else
            {
                Console.WriteLine("No data found.");
            }
            reader.Close();
            return studentsList;
        }

        private List<Student> ReadStudents(SqlDataReader reader)
        {
            var studentsList = new List<Student>();
            while (reader.Read())
            {
                var student = new Student(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetDateTime(3)
                );
                studentsList.Add(student);
            }
            return studentsList;
        }
    }
}
