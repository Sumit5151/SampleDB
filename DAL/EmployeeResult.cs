using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleDB.DAL
{
   
    public class EmployeeResult
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int Gender { get; set; }
        public int DepartmentId { get; set; }
        public string DepartMentName { get; set; }
    }
}
