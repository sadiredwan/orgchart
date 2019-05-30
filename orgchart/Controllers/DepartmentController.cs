using orgchart.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace orgchart.Controllers
{
    public class DepartmentController : Controller
    {
        /// <summary>
        /// Index page 
        /// for show all designations list
        /// </summary>
        /// <returns></returns>
        // GET: Department
        public ActionResult Index()
        {
            //Get Current UserName
            int id = Convert.ToInt32(User.Identity.Name);

            //Set  Message
            String message = "";

            //Check Tempdate error or successmessage 
            if (TempData["message"] != null)
            {
                message = TempData["message"].ToString();
            }

            //view data send message
            ViewData["message"] = message;

            //Find all Departments list into a list and bind/pass to view
            List<Department> lstDepartments = new DBContext().departments.ToList();
            return View(lstDepartments);
        }

        /// <summary>
        /// Create Page for Department 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult create(String id)
        {
            //Get Current UserName
            int user_id = Convert.ToInt32(User.Identity.Name);

            //Set  Message
            String message = "";

            int dID = Convert.ToInt32(id);

            //pass model to view
            Department mdlDepartment = new Department();

            //Check if id doesnot null
            if (id != null)
            {
                //check if department already exist
                if (new DBContext().departments.ToList().FindAll(x => x.id == dID).Count > 0)
                {
                    //pass model to view with department info
                    mdlDepartment = new DBContext().departments.Find(dID);
                }
            }

            //Check Temp error or successmessage 
            if (TempData["message"] != null)
            {
                message = TempData["message"].ToString();
            }

            //view data send message
            ViewData["message"] = message;

            return View(mdlDepartment);
        }

        /// <summary>
        /// Store Department 
        /// Create or Update
        /// </summary>
        /// <param name="deptMdl'"></param>
        /// <returns></returns>
        public ActionResult store(Department deptMdl)
        {
            //Get Current UserName
            int id = Convert.ToInt32(User.Identity.Name);

            //Check Model state is valid or not
            if (ModelState.IsValid)
            {

                //check if already exist then update
                if (new DBContext().departments.ToList().FindAll(x => x.id == deptMdl.id).Count > 0)
                {
                    //Update Department
                    //deptMdl.updated_at = DateTime.Now;
                    using (var contxt = new DBContext())
                    {
                        contxt.departments.Attach(deptMdl);
                        contxt.Entry(deptMdl).State = EntityState.Modified;
                        contxt.SaveChanges();

                    }
                    //Set temp data Success registration message
                    TempData["message"] = Utility.SUCCESS_MESSAGE + "Department Updated successfully";
                }
                else
                {
                    //Add new Department
                    using (var contxt = new DBContext())
                    {
                        //Set Datetime
                        deptMdl.created_at = DateTime.Now;
                        //Add New Department
                        contxt.departments.Add(deptMdl);
                        contxt.SaveChanges();

                    }
                    //Set temp data Success registration message
                    TempData["message"] = Utility.SUCCESS_MESSAGE + "Department Add Successfully";
                }

                return RedirectToAction("index");
            }
            else
            {
                string errors = errorstate.errors(ModelState);
                //Set temp data for wrong credentials
                TempData["message"] = Utility.FAILED_MESSAGE + "" + errors;

                //redirect back to Registration page 
                return RedirectToAction("create");
            }
        }


        public ActionResult delete(String id)
        {
            //Check if  not null
            if (id != null)
            {
                int dID = Convert.ToInt32(id);

                //Find the Department 
                Department deptMdl = new DBContext().departments.Find(dID);


                //Delete The Department
                using (var contxt = new DBContext())
                {
                    contxt.Entry(deptMdl).State = EntityState.Deleted;
                    contxt.SaveChanges();
                }

                //Set temp data Success registration message
                TempData["message"] = Utility.SUCCESS_MESSAGE + "Department Deleted Successfully";

            }
            else
            {
                //Set temp data Success registration message
                TempData["message"] = Utility.FAILED_MESSAGE + "Something went wrong";
            }
            return RedirectToAction("index");
        }


    }
}