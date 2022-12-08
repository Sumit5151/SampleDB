using Microsoft.AspNetCore.Mvc;
using SampleDB.DAL;

namespace SampleDB.Controllers
{
    public class DDLController : Controller
    {
        private readonly AppDbContext context;
        public DDLController(AppDbContext _context)
        {
            this.context = _context;
        }
        public IActionResult Index()
        {

            ViewBag.Countries = context.Countries;
            return View();
        }

        public JsonResult GetStates(int countryId)
        {
            var states = context.States.Where(x => x.CountryId == countryId).ToList();
            return Json(states);
        }

        public JsonResult GetDistricts(int stateId)
        {
            var districts = context.Districts.Where(x => x.StateId == stateId).ToList();
            return Json(districts);
        }
    }
}
