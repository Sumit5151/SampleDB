using Microsoft.AspNetCore.Mvc;
using SampleDB.BAL.EmployeeRepository;
using SampleDB.DAL;

namespace SampleDB.Component
{
    public class HeadCountViewComponent:ViewComponent
    {
        private readonly IEmployeeRepository employeeRepository;

        public HeadCountViewComponent(IEmployeeRepository _employeeRepository)
        {
            this.employeeRepository = _employeeRepository;            
        }



        public IViewComponentResult Invoke(Department? department= null)
        {
            var deptcount =employeeRepository.GetDeptCount(department);
            return View(deptcount);
        }
    }
}
