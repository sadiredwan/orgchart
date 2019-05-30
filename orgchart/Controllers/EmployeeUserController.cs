using orgchart.Models;
using orgchart.SecurityAuth;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace orgchart.Controllers
{
    public class EmployeeUserController : Controller
    {
        // GET: EmployeeUser
        public ActionResult Index()
        {
            //Get Current UserName
            int id = Convert.ToInt32(User.Identity.Name);
            //Current Compamy
            company find_company = new DBContext().companies.ToList().Find(x => x.user_id == id);

            //Set  Message
            String message = "";

            //Check Tempdate error or successmessage 
            if (TempData["message"] != null)
            {
                message = TempData["message"].ToString();
            }

            //view data send message
            ViewData["message"] = message;

            //Find all employees list into a list and bind/pass to view
            List<Employee> lstEmployeesForUser = new DBContext().employees.ToList().FindAll(x => x.company_id == find_company.id);
            return View(lstEmployeesForUser);
        }

        public ActionResult assign(String empID)
        {
            //Get Current UserName
            int user_id = Convert.ToInt32(User.Identity.Name);
            //Current Compamy
            company find_company = new DBContext().companies.ToList().Find(x => x.user_id == user_id);

            //Set  Message
            String message = "";


            int eID = Convert.ToInt32(empID);


            //pass model to view
            Employee mdlEmployee = new Employee();

            //Check if id doesnot null
            if (empID != null && empID != "0")
            {
                //check if Employee already exist
                if (new DBContext().employees.ToList().FindAll(x => x.id == eID).Count > 0)
                {
                    //pass model to view with Employee info
                    mdlEmployee = new DBContext().employees.Find(eID);
                }
            }

            //Check Temp error or successmessage 
            if (TempData["message"] != null)
            {
                message = TempData["message"].ToString();
            }


            //view data send message
            ViewData["message"] = message;
            EmployeeUserViewModel vm = new EmployeeUserViewModel();
            vm.VM_EMPLOYEE = mdlEmployee;
            vm.VM_USER = new User();
            vm.VM_EMPLOYEEUSER = new EmployeeUser();

            return View(vm);
        }

        public ActionResult store(EmployeeUserViewModel euViewModel)
        {
            //Get Current UserName
            int id = Convert.ToInt32(User.Identity.Name);
            //Current Compamy
            company find_company = new DBContext().companies.ToList().Find(x => x.user_id == id);

            //Check Model state is valid or not
            if (ModelState.IsValid)
            {

                //Check if already exists
                if (new DBContext().employee_users.ToList().FindAll(x => x.employee_id == euViewModel.VM_EMPLOYEE.id).Count > 0)
                {
                    //TO DO: DO NOTHING?
                }
                else
                {
                    //Add new EmployeeUser
                    using (var contxt = new DBContext())
                    {
                        //Set Datetime
                        //EmployeeMdl.created_at = DateTime.Now;
                        //set comapmny id
                        //euViewModel.VM_EMPLOYEE.company_id = find_company.id;
                        //Add New EmployeeUser
                        EmployeeUser employeeUser = new EmployeeUser
                        {
                            employee_id = euViewModel.VM_EMPLOYEEUSER.employee_id,
                            user_id = euViewModel.VM_EMPLOYEEUSER.user_id
                        };
                        contxt.employee_users.Add(employeeUser);
                        contxt.SaveChanges();

                    }
                    //Set temp data Success registration message
                    TempData["message"] = Utility.SUCCESS_MESSAGE + "Employee Assigned Successfully";
                }

                return RedirectToAction("index");
            }
            //else
            //{
            //    string errors = errorstate.errors(ModelState);
            //    //Set temp data for wrong credentials
            //    TempData["message"] = Utility.FAILED_MESSAGE + "" + errors;

            //    String param_id = euViewModel.VM_EMPLOYEE.id.ToString();
            //    //redirect back to assign page 
            //    return RedirectToAction("assign", new { empID = param_id });
            //}
            return RedirectToAction("index");
        }
    }
}