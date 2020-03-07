using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentManager.Common.Aggregates.StudentAggregate;
using StudentManager.DataAccess.SQL.Repositories;

namespace StudentManager.DataAccess.SQL.Tests
{
    [TestClass]
    public class StudentRepositoryTests
    {
        [TestMethod]
        public void ShouldOpenConnection ()
        {
            var result = false;
            var repository = new StudentRepository();
            var openConnection = repository.GetAll();
            if (openConnection != null)
            {
                result = true;
            }
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void GetAll_GivenThreeElementsFromDB_ShouldReturnAListWithThreeElements()
        {
            var repository = new StudentRepository();
            var result = repository.GetAll().ToList();
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void Create_GivenThreeElementsFromDB_ShouldReturnAListWithFourElements()
        {
            var student = new Student
            {
                Id = 4,
                Guid = Guid.NewGuid(),
                Name = "Nancy",
                LastName = "Jimenez",
                BirthDate = DateTime.Parse("31/07/1962")
            };
            var repository = new StudentRepository();
            repository.Create(student);
            var result = repository.GetAll();
            Assert.AreEqual(4, result.Count());
        }

        [TestMethod]
        public void Update_GivenFourElementsFromDB_ShouldReturnAListWithFourElements()
        {
            var student = new Student
            {
                Id = 1,
                Guid = Guid.NewGuid(),
                Name = "Lola",
                LastName = "Perez",
                BirthDate = DateTime.Parse("31/07/1998")
            };
            var repository = new StudentRepository();
            repository.Update(student);
            var result = repository.GetAll();
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public void Delete_GivenFourElementsFromDB_ShouldReturnAListWithThreeElements()
        {
            var repository = new StudentRepository();
            repository.DeleteById(4);
            var result = repository.GetAll();
            Assert.AreEqual(3, result.Count());
        }

    }
}
