using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentManager.DataAccess.SQL.Repositories;

namespace StudentManager.DataAccess.SQL.Tests
{
    [TestClass]
    public class StudentRepositoryTests
    {
        [TestMethod]
        public void ShouldOpenConnection ()
        {
            var repository = new StudentRepository();
            repository.GetAll();
        }
    }
}
