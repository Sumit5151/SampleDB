using System.ComponentModel.DataAnnotations.Schema;

namespace SampleDB.DAL
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Salary { get; set; }
        public int GenderId { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department? Department { get; set; } = new Department();
        public virtual Gender? Genders { get; set; } 

    }
}
