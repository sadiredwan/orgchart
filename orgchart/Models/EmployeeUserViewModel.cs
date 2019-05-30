using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace orgchart.Models
{
    public class EmployeeUserViewModel
    {
        public List<User> VM_USERS { get; set; }
        public User VM_USER { get; set; }
        public List<Employee> VM_EMPLOYEES { get; set; }
        public Employee VM_EMPLOYEE { get; set; }

        public List<EmployeeUser> VM_EMPLOYEEUSERS { get; set; }
        public EmployeeUser VM_EMPLOYEEUSER { get; set; }
    }
}