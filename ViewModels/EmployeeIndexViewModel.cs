using SampleDB.Models;

namespace SampleDB.ViewModels
{
    public class EmployeeIndexViewModel:PagedList
    {
      public  List<EmployeeViewModel> EmployeeViewModels { get; set; }= new List<EmployeeViewModel>();
        

    }
}
