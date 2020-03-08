using StudentManager.Common.Seedwork;
using System;

namespace StudentManager.Common.Aggregates.StudentAggregate
{
    public class Student : Entity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Guid Guid { get; set; }

        public Student()
        {
        }
    }
}
