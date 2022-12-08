using Microsoft.AspNetCore.Mvc;
using SampleDB.BAL.EmployeeRepository;
using SampleDB.DAL;

namespace SampleDB.Controllers
{
    public class PassingDataDemoController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;
        public PassingDataDemoController(IEmployeeRepository _employeeRepository)
        {
            this.employeeRepository = _employeeRepository;
        }
        public IActionResult Index()
        {

            employeeRepository.LinqTest();

            ViewBag.Name = "Shyam";
            @ViewData["Age"] = 37;
            @TempData["Message"] = "This is Message";
            @TempData["Message1"] = "This is Message";
            @TempData["Message2"] = "This is Message";
            @TempData["Message3"] = "This is Message";
            @TempData["Message4"] = "This is Message";

            var employee = new 
            {
                Id = 1,
                Name = "Sumit",
                GenderId = 2
            };

            ViewData["emp"] = employee;


            return View();
        }



        public IActionResult Create()
        {
            return View();
        }


    }
}
