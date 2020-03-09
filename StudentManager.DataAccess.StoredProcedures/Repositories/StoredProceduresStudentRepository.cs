using log4net;
using StudentManager.Common.Aggregates.StudentAggregate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace StudentManager.DataAccess.StoredProcedures.Repositories
{
    public class StoredProceduresStudentRepository : IStudentRepository
    {
        private readonly string connectionString = "Server=.;Database=StudentManager;User Id=sa;Password=yourStrong(!)Password;";
        private static readonly ILog logger = LogManager.GetLogger(typeof(StoredProceduresStudentRepository));
        public IEnumerable<Student> GetAll()
        {
            List<Student> studentsList = null;
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    string procedure = @"dbo.[GetAllStudents]";
                    SqlCommand command = new SqlCommand(procedure, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    connection.Open();
                    studentsList = ReadDatabase(command);
                }
                catch (InvalidOperationException e)
                {
                    logger.Error(e);
                    throw;
                }
                catch (ArgumentException e)
                {
                    logger.Error(e);
                    throw;
                }
                catch (SqlException e)
                {
                    logger.Error(e);
                    throw;
                }
            }
            return studentsList;
        }


        public Student Create(Student entity)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    string procedure = @"dbo.[AddStudent]";
                    SqlCommand command = new SqlCommand(procedure, connection);
                    command.Parameters.AddWithValue("@student_name", entity.Name);
                    command.Parameters.AddWithValue("@student_surname", entity.LastName);
                    command.Parameters.AddWithValue("@student_birth_date", entity.BirthDate);
                    command.Parameters.AddWithValue("@student_guid", entity.Guid);
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    command.ExecuteNonQuery();

                }
                catch (InvalidCastException e)
                {
                    logger.Error(e);
                    throw;
                }
                catch (ArgumentException e)
                {
                    logger.Error(e);
                    throw;
                }
                catch (InvalidOperationException e)
                {
                    logger.Error(e);
                    throw;
                }
                catch (SqlException e)
                {
                    logger.Error(e);
                    throw;
                }
                catch (IOException e)
                {
                    logger.Error(e);
                    throw;
                }
            }
            return entity;
        }

        public Student Update(Student entity)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    string procedure = @"dbo.[UpdateStudent]";
                    SqlCommand command = new SqlCommand(procedure, connection);
                    command.Parameters.AddWithValue("@id", entity.Id);
                    command.Parameters.AddWithValue("@student_name", entity.Name);
                    command.Parameters.AddWithValue("@student_surname", entity.LastName);
                    command.Parameters.AddWithValue("@student_birth_date", entity.BirthDate);
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    command.ExecuteNonQuery();
                }
                catch (InvalidCastException e)
                {
                    logger.Error(e);
                    throw;
                }
                catch (ArgumentException e)
                {
                    logger.Error(e);
                    throw;
                }
                catch (InvalidOperationException e)
                {
                    logger.Error(e);
                    throw;
                }
                catch (SqlException e)
                {
                    logger.Error(e);
                    throw;
                }
                catch (IOException e)
                {
                    logger.Error(e);
                    throw;
                }
            }
            return entity;
        }

        public Student DeleteById(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string procedure = @"dbo.[DeleteStudent]";
                SqlCommand command = new SqlCommand(procedure, connection);
                try
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@id", id);
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }
                catch (InvalidCastException e)
                {
                    logger.Error(e);
                    throw;
                }
                catch (ArgumentException e)
                {
                    logger.Error(e);
                    throw;
                }
                catch (InvalidOperationException e)
                {
                    logger.Error(e);
                    throw;
                }
                catch (SqlException e)
                {
                    logger.Error(e);
                    throw;
                }
                catch (IOException e)
                {
                    logger.Error(e);
                    throw;
                }
            }
            var studentsList = GetAll();
            
            return studentsList.FirstOrDefault(x => x.Id == id);
        }

        public List<Student> ReadDatabase(SqlCommand command)
        {
            var studentsList = new List<Student>();
            try
            {
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        studentsList = ReadStudents(reader);
                    }
                    else
                    {
                        Console.WriteLine("No data found.");
                    }
                }
            }
            catch (InvalidCastException e)
            {
                logger.Error(e);
                throw;
            }
            catch (InvalidOperationException e)
            {
                logger.Error(e);
                throw;
            }
            catch (SqlException e)
            {
                logger.Error(e);
                throw;
            }
            catch (IOException e)
            {
                logger.Error(e);
                throw;
            }
            return studentsList;
        }

        private List<Student> ReadStudents(SqlDataReader reader)
        {
            var studentsList = new List<Student>();
            try
            {
                while (reader.Read())
                {
                    var student = new Student
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        LastName = reader.GetString(2),
                        BirthDate = reader.GetDateTime(3),
                        Guid = reader.GetGuid(4)
                    };
                    studentsList.Add(student);
                }
            }
            catch (SqlException e)
            {
                logger.Error(e);
                throw;
            }
            return studentsList;
        }
    }
}
