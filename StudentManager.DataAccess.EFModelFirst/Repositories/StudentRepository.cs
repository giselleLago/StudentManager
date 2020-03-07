using StudentManager.Common.Aggregates.StudentAggregate;
using System;
using System.Collections.Generic;

namespace StudentManager.DataAccess.EFModelFirst.Repositories
{
    public class StudentRepository : IStudentRepository
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

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
