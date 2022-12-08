using SampleDB.DAL;
using SampleDB.Models;

namespace SampleDB.ViewModels
{
    public class EmployeeViewModel
    {
        private Employee employee;

        public EmployeeViewModel()
        {
        }

        public EmployeeViewModel(Employee employee)
        {
            this.Id= employee.Id;
            this.Name= employee.Name;
            this.Gender= (GenderEnum)employee.GenderId;
            this.DepartmentId= employee.DepartmentId;
            this.DepartmentName = employee.Department.Name;
            //this.Department= employee.Department;
            var name = employee.Department.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public GenderEnum Gender { get; set; }
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public Department? Department { get; set; }
    }
}
