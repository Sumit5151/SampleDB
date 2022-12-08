using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SampleDB.DAL;
using SampleDB.Extension;
using SampleDB.ViewModels;

namespace SampleDB.BAL.EmployeeRepository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext db;
        private readonly IMemoryCache memoryCache;
        private IHttpContextAccessor httpContextAccessor;


        public EmployeeRepository(AppDbContext _db,
            IMemoryCache _memoryCache,
            IHttpContextAccessor _httpContextAccessor)
        {
            this.db = _db;
            this.memoryCache = _memoryCache;
            this.httpContextAccessor = _httpContextAccessor;
        }
        public EmployeeIndexViewModel GetAlL()
        {
            var name = httpContextAccessor.HttpContext.Session.GetString("Name");
            //IQueryable<Employee> employees = db.Employees.Include(x=>x.Department).Where(x => x.GenderId == 1);
            //var employees = list.Take<Employee>(5);
            var employees = db.Employees.ToList<Employee>();

            //db.Entry(employees).Reference("Department").Load();


            EmployeeIndexViewModel EmployeeIndexViewModel = new EmployeeIndexViewModel();
            foreach (var employee in employees)
            {
                EmployeeIndexViewModel.EmployeeViewModels.Add(new EmployeeViewModel(employee));

            }
            return EmployeeIndexViewModel;
        }

        public List<Department> GetAllDepartments()
        {
            return db.Departments.ToList();

        }

        public void Create(EmployeeViewModel employeeViewModel)
        {

            var employee = new Employee();
            employee.ConverViewModelToDataModel(employeeViewModel);
            employee.Department = null;
            employee.Genders = null;
            db.Employees.Add(employee);
            db.SaveChanges();
        }

        public void Update(EmployeeViewModel employeeViewModel)
        {
            var employee = new Employee();
            employee.ConverViewModelToDataModel(employeeViewModel);
            //var attachedEmployee = db.Employees.Attach(employee);
            //attachedEmployee.State = EntityState.Modified;

            db.Entry(employee).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            //var employee = db.Employees.Find(id);
            //if (employee != null)
            //{
            //    db.Employees.Remove(employee);
            //    db.SaveChanges();
            //}


            var employee = new Employee { Id = id };
            db.Entry(employee).State = EntityState.Deleted;
            db.SaveChanges();
        }

        public EmployeeViewModel Get(int id)
        {
            var employee = db.Employees.Find(id);
            var employeeViewModel = new EmployeeViewModel(employee);
            return employeeViewModel;
        }


        public EmployeeIndexViewModel GetEmployees(int CurrentPage, int pageSize)
        {
            EmployeeIndexViewModel employeeIndexViewModel;
            string cacheKey = "EmployeeList";
            if (memoryCache.TryGetValue(cacheKey, out employeeIndexViewModel) == false)
            {

                var memoryCacheEntryOptions = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(60),
                    SlidingExpiration = TimeSpan.FromSeconds(20),
                    Priority = CacheItemPriority.High
                };

                var employees = db.Employees.Skip((CurrentPage - 1) * 10).Take(pageSize).Include(x => x.Department).ToList();

                employeeIndexViewModel = new EmployeeIndexViewModel();
                employeeIndexViewModel.PageSize = pageSize;
                employeeIndexViewModel.CurrentPage = CurrentPage;
                employeeIndexViewModel.TotalCount = db.Employees.Count();

                employeeIndexViewModel.EmployeeViewModels = new List<EmployeeViewModel>();
                foreach (var employee in employees)
                {
                    employeeIndexViewModel.EmployeeViewModels.Add(new EmployeeViewModel(employee));
                }
                memoryCache.Set(cacheKey, employeeIndexViewModel, memoryCacheEntryOptions);
            }
            return employeeIndexViewModel;
        }


        public void GetBySp(int id)
        {
            // var employee = db.Employees.FromSqlRaw<Employee>("Select * FROM Employees where id = {0}",id).FirstOrDefault();

            //var employeeViewModel = new EmployeeViewModel(employee);
            //return employeeViewModel;


            var employees = db.Employees.ToList<Employee>();

            foreach (var emp in employees)
            {
                var dept = emp.Department.Name;
            }
        }



        public void LinqTest()
        {
            var employee = db.Employees.GroupBy(x => x.Salary)
                .OrderByDescending(x => x.Key).Skip(1).Take(1).Select(x => x.Key);


            var salary = employee.FirstOrDefault();
        }

        public List<DeptCountModel> GetDeptCount(Department? department)
        {

            IEnumerable<Employee> query = db.Employees.Include(x => x.Department);
            if (department != null)
            {
                query = query.Where(x => x.Department.Name == department.Name);
            }
            var deptCountModels = query
                                .GroupBy(x => x.Department.Name)
                                .Select(g => new DeptCountModel
                                {
                                    DeptName = g.Key,
                                    DeptCount = g.Count()
                                }).ToList();
            return deptCountModels;
        }
    }
}
