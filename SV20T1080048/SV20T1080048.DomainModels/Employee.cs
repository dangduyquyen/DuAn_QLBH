using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV20T1080048.DomainModels
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FullName { get; set; } = "";
        public DateTime BirthDate { get; set; }
        public string Address { get; set; } = "";
        public string Phone { set; get; } = "";
        public string Email { set; get; } = "";
        public string Password { set; get; } = "";
        public string Photo { set; get; } = "";
        public bool IsWorking { get; set; } = true;
    }
}
