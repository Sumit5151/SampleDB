using SampleDB.DAL;
using SampleDB.ViewModels;

namespace SampleDB.Extension
{
    public static class EmployeeExtensionMethods
    {
        public static void ConverViewModelToDataModel(this Employee employee, EmployeeViewModel employeeViewModel)
        {
            employee.Id = employeeViewModel.Id;
            employee.Name= employeeViewModel.Name;
            employee.GenderId = (int)employeeViewModel.Gender;
            employee.DepartmentId = employeeViewModel.DepartmentId;
        }
    }
}
