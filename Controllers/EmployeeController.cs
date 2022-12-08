using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SampleDB.BAL.EmployeeRepository;
using SampleDB.ViewModels;
using SampleDB.Extension;
using Newtonsoft.Json;

namespace SampleDB.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMemoryCache memoryCache;

        public EmployeeController(IEmployeeRepository _employeeRepository, 
            IMemoryCache _memoryCache)
        {
            this.employeeRepository = _employeeRepository;
            this.memoryCache = _memoryCache;
        }
        public IActionResult Index(int? pageNumber = 1)
        {
            HttpContext.Session.SetString("Name", "Sumit");
            HttpContext.Session.SetInt32("Age", 20);

           

            ViewData["ViewDataMessage"] = "This is ViewData message";
            TempData["TempDataMessage"] = "This is TempData message";
            // employeeRepository.GetBySp(4);
            var employees = employeeRepository.GetAlL();

            HttpContext.Session.SetString("Employee", JsonConvert.SerializeObject(employees));


            //var employeeViewmodels = employees.EmployeeViewModels.ToList();
            //var employee = employeeRepository.Get(33); 

            //var employees = employeeRepository.GetEmployees(pageNumber.Value, 10);

            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {

            var name = HttpContext.Session.GetString("Name");
            var age = HttpContext.Session.GetInt32("Age");


            EmployeeViewModel employeeViewModel = new EmployeeViewModel();
            ViewBag.Departments = employeeRepository.GetAllDepartments();
            return View("CreateEmployee", employeeViewModel);
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeViewModel)
        {
            
            if (ModelState.IsValid == true)
            {
                employeeRepository.Create(employeeViewModel);
                TempData["Message"] = "Record Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = employeeRepository.GetAllDepartments();


            
            

            return View("CreateEmployee", employeeViewModel);
        }


        [HttpGet]
        public IActionResult Update(int id)
        {
            var employeeViewModel = employeeRepository.Get(id);
            var departments = employeeRepository.GetAllDepartments();
            ViewBag.Departments = departments;
            return View(employeeViewModel);
        }

        [HttpPost]
        public IActionResult Update(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid == true)
            {
                employeeRepository.Update(employeeViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(employeeViewModel);
        }


        public IActionResult Delete(int id)
        {
            employeeRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
