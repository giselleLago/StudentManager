using StudentManager.Common.Aggregates.StudentAggregate;
using System;
using System.Collections.Generic;

namespace StudentManager.DataAccess.Dapper.Repositories
{
    public class DapperStudentRepository : IStudentRepository
    {
        public IEnumerable<Student> GetAll()
        {
            throw new NotImplementedException();
        }

        public Student Create(Student entity)
        {
            throw new NotImplementedException();
        }

        public Student Update(Student entity)
        {
            throw new NotImplementedException();
        }

        public Student DeleteById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
