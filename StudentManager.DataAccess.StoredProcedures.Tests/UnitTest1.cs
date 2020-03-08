using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentManager.Common.Aggregates.StudentAggregate;
using StudentManager.DataAccess.StoredProcedures.Repositories;

namespace StudentManager.DataAccess.StoredProcedures.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            string connectionString = "Server=.;Database=StudentManager;User Id=sa;Password=yourStrong(!)Password;";
            StoredProceduresStudentRepository repository = new StoredProceduresStudentRepository();
        }

        [TestInitialize]
        public void Setup()
        {
            StoredProceduresStudentRepository repository = new StoredProceduresStudentRepository();
            Student student1 = new Student { Id = 1, Name = "Albert", LastName = "Riera", BirthDate = DateTime.Parse("10/10/2010"), Guid = Guid.NewGuid() };
            Student student2 = new Student { Id = 2, Name = "Oscar", LastName = "Perez", BirthDate = DateTime.Parse("10/10/2010"), Guid = Guid.NewGuid() };
            Student student3 = new Student { Id = 3, Name = "fasdfasdf", LastName = "fdsa", BirthDate = DateTime.Parse("10/10/2010"), Guid = Guid.NewGuid() };
            Student student4 = new Student { Id = 4, Name = "ghhh", LastName = "rtrr", BirthDate = DateTime.Parse("10/10/2010"), Guid = Guid.NewGuid() };
            Student student5 = new Student { Id = 5, Name = "nnn", LastName = "fgfgs", BirthDate = DateTime.Parse("10/10/2010"), Guid = Guid.NewGuid() };
            repository.Create(student1);
            repository.Create(student2);
            repository.Create(student3);
            repository.Create(student4);
            repository.Create(student5);
        }

        [ClassCleanup]
        public static void TestFixtureTearDown()
        {
            string connectionString = "Server=.;Database=StudentManager;User Id=sa;Password=yourStrong(!)Password;";
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Student";
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.Text;
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        [TestMethod()]
        public void CreateTest()
        {
            StoredProceduresStudentRepository repository = new StoredProceduresStudentRepository();
            Student studentToAdd = new Student { Id = 6, Name = "added", LastName = "student", BirthDate = DateTime.Parse("10/10/2010"), Guid = Guid.NewGuid() };
            repository.Create(studentToAdd);
            var studentsList = repository.GetAll().ToList();
            Assert.IsTrue(studentsList.Count() == 6);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            StoredProceduresStudentRepository repository = new StoredProceduresStudentRepository();
            Student studentToUpdate = new Student { Id = 2, Name = "Updated", LastName = "Student", BirthDate = DateTime.Parse("10/10/2010") };
            repository.Update(studentToUpdate);
            var studentsList = repository.GetAll().ToList();
            var updatedStudent = studentsList.Find(x => x.Id == 2);
            Assert.IsTrue(updatedStudent.Name == "Updated");
        }

        [TestMethod()]
        public void DeleteTest()
        {
            StoredProceduresStudentRepository repository = new StoredProceduresStudentRepository();
            repository.DeleteById(5);
            var studentsList = repository.GetAll().ToList();
            var updatedStudent = studentsList.Find(x => x.Id == 2);
            Assert.IsNull(studentsList.Find(x=>x.Id==5));
        }

        [TestMethod()]
        public void GetAllTest()
        {
            StoredProceduresStudentRepository repository = new StoredProceduresStudentRepository();
            var studentsList = repository.GetAll().ToList();
            Assert.IsTrue(studentsList.Count() == 5);
        }
    }
}
