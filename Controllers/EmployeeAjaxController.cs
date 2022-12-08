using Microsoft.AspNetCore.Mvc;
using SampleDB.BAL.EmployeeRepository;
using SampleDB.ViewModels;

namespace SampleDB.Controllers
{
    public class EmployeeAjaxController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;
       

        public EmployeeAjaxController(IEmployeeRepository _employeeRepository)
        {
            this.employeeRepository = _employeeRepository;
            
        }


        public IActionResult Index()
        {
            return View();
        }


        public PartialViewResult EmployeeList()
        {
            var employee = employeeRepository.GetAlL();

            return PartialView("_EmployeeList", employee);
        }


        [HttpGet]
        public PartialViewResult Create()
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel();
            ViewBag.Departments = employeeRepository.GetAllDepartments();

            return PartialView("_Create", employeeViewModel);
        }

        [HttpPost]
        public PartialViewResult Create(EmployeeViewModel employeeViewModel)
        {
            employeeRepository.Create(employeeViewModel);

            var employee = employeeRepository.GetAlL();

            return PartialView("_EmployeeList", employee);
        
        }


        [HttpGet]
        public PartialViewResult Update(int id)
        {
            EmployeeViewModel employeeViewModel = employeeRepository.Get(id);
            ViewBag.Departments = employeeRepository.GetAllDepartments();

            return PartialView("_Update", employeeViewModel);
        }

        [HttpPost]
        public PartialViewResult Update(EmployeeViewModel employeeViewModel)
        {
            employeeRepository.Update(employeeViewModel);
            var employee = employeeRepository.GetAlL();
            return PartialView("_EmployeeList", employee);

        }


        [HttpGet]
        public PartialViewResult Delete(int id)
        {
            employeeRepository.Delete(id);           
            var employee = employeeRepository.GetAlL();
            return PartialView("_EmployeeList", employee);
        }
    }
}
