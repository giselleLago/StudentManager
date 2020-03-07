using StudentManager.Common.Seedwork;

namespace StudentManager.Common.Aggregates.StudentAggregate
{
    public class Student : Entity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}
