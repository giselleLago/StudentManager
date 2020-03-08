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
            StudentRepository repository = new StudentRepository();
        }

        [TestInitialize]
        public void Setup()
        {
            StudentRepository repository = new StudentRepository();
            Student student1 = new Student(1, "Albert", "Riera", DateTime.Parse("10/10/2010"), Guid.NewGuid());
            Student student2 = new Student(2, "Oscar", "Perez", DateTime.Parse("10/10/2010"), Guid.NewGuid());
            Student student3 = new Student(3, "fasdfasdf", "fdsa", DateTime.Parse("10/10/2010"), Guid.NewGuid());
            Student student4 = new Student(4, "ghhh", "rtrr", DateTime.Parse("10/10/2010"), Guid.NewGuid());
            Student student5 = new Student(5, "nnn", "fgfgs", DateTime.Parse("10/10/2010"), Guid.NewGuid());
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
            StudentRepository repository = new StudentRepository();
            Student studentToAdd = new Student(6, "added", "student", DateTime.Parse("10/10/2010"), Guid.NewGuid());
            repository.Create(studentToAdd);
            var studentsList = repository.GetAll().ToList();
            Assert.IsTrue(studentsList.Count() == 6);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            StudentRepository repository = new StudentRepository();
            Student studentToAdd = new Student(2, "Updated", "Student", DateTime.Parse("10/10/2010"));
            repository.Update(studentToAdd);
            var studentsList = repository.GetAll().ToList();
            var updatedStudent = studentsList.Find(x => x.Id == 2);
            Assert.IsTrue(updatedStudent.Name == "Updated");
        }

        [TestMethod()]
        public void DeleteTest()
        {
            StudentRepository repository = new StudentRepository();
            repository.DeleteById(5);
            var studentsList = repository.GetAll().ToList();
            var updatedStudent = studentsList.Find(x => x.Id == 2);
            Assert.IsNull(studentsList.Find(x=>x.Id==5));
        }

        [TestMethod()]
        public void GetAllTest()
        {
            StudentRepository repository = new StudentRepository();
            var studentsList = repository.GetAll().ToList();
            Assert.IsTrue(studentsList.Count() == 5);
        }
    }
}
