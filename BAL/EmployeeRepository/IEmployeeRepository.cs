using SampleDB.DAL;
using SampleDB.ViewModels;

namespace SampleDB.BAL.EmployeeRepository
{
    public interface IEmployeeRepository
    {
        EmployeeIndexViewModel GetAlL();
        EmployeeViewModel Get(int id);
        List<Department> GetAllDepartments();
        void Create(EmployeeViewModel employeeViewModel) ;
        void Update(EmployeeViewModel employeeViewModel) ;
        void Delete(int id);
        EmployeeIndexViewModel GetEmployees(int PageNumber, int pageSize);
        void GetBySp(int id);
        List<DeptCountModel> GetDeptCount(Department? department=null);
        void LinqTest();
    }
}
