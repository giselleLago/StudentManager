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

        public Student(int studentId, string name, string lastName, DateTime birthDate, Guid guid)
        {
            Name = name;
            LastName = lastName;
            BirthDate = birthDate;
            Guid = guid;
        }

        public Student(int studentId, string name, string lastName, DateTime birthDate)
        {
            Name = name;
            LastName = lastName;
            BirthDate = birthDate;
        }
    }
}
