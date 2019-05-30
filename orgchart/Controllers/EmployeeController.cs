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
    public class EmployeeController : Controller
    {   /// <summary>
        /// Index page 
        /// for show all employees list
        /// </summary>  
        /// <returns></returns>
        // GET: Employee

        //[SecurityAuthAuthorize(AccessLevels = 
        //    new AccessLevel[] {
        //    AccessLevel.View,
        //    AccessLevel.Create,
        //    AccessLevel.Delete,
        //    AccessLevel.Update })]
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
            List<Employee> lstemployees = new DBContext().employees.ToList().FindAll(x => x.company_id == find_company.id);
            return View(lstemployees);
        }

        /// <summary>
        /// Create Page for Employee 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult create(String id)
        {
            //Get Current UserName
            int user_id = Convert.ToInt32(User.Identity.Name);
            //Current Compamy
            company find_company = new DBContext().companies.ToList().Find(x => x.user_id == user_id);

            //Set  Message
            String message = "";


            int dID = Convert.ToInt32(id);
            

            //pass model to view
            Employee mdlEmployee = new Employee();

            //Check if id doesnot null
            if (id != null && id != "0")
            {
                //check if Employee already exist
                if (new DBContext().employees.ToList().FindAll(x => x.id == dID).Count > 0)
                {
                    //pass model to view with Employee info
                    mdlEmployee = new DBContext().employees.Find(dID);
                }
            }

            //Check Temp error or successmessage 
            if (TempData["message"] != null)
            {
                message = TempData["message"].ToString();
            }


            //view data send message
            ViewData["message"] = message;

            return View(mdlEmployee);
        }

        /// <summary>
        /// Store Desiognation 
        /// Create or Update
        /// </summary>
        /// <param name="EmployeeMdl"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public ActionResult store(Employee EmployeeMdl)
        {
            
            //Get Current UserName
            int id = Convert.ToInt32(User.Identity.Name);
            //Current Compamy
            company find_company = new DBContext().companies.ToList().Find(x => x.user_id == id);

            String imgFileLocation = "";

            //Check Model state is valid or not
            if (ModelState.IsValid)
            {
                
                //Check File
                if (Request.Files.Count>0)
                {
                    HttpPostedFileBase file= Request.Files[0];

                    double file_size = Convert.ToDouble(file.ContentLength) / 1000000;

                    //Check fileupload or not
                    if (file.ContentLength > 0)
                    {
                        //Check Upload file length maximum  1MB
                        if (file_size <= 1)
                        {
                            //Check file extension jpg or png only
                            string extension = file.FileName.Split('.')[1];
                            if (extension.Equals("jpg") || extension.Equals("png"))
                            {
                                var file_save_path = "/content/images/uploads";
                                var fileName = Path.GetFileName(file.FileName);
                                string FileExtn = Path.GetExtension(file.FileName);
                                fileName = Guid.NewGuid().ToString().Replace("-", "") + FileExtn;
                                String fileLocation = Path.Combine(Server.MapPath("~/" + file_save_path), fileName);
                                file.SaveAs(fileLocation);
                                imgFileLocation = string.Format("{0}/{1}", file_save_path, fileName);
                            }
                            else
                            {
                                //Set temp data Error message
                                TempData["message"] = Utility.FAILED_MESSAGE + " File Must be .JPG or .PNG format";
                                return RedirectToAction("create", new { id = EmployeeMdl.id });
                            }

                        }
                        else
                        {
                            //Set temp data Error message
                            TempData["message"] = Utility.FAILED_MESSAGE + " Maximum FIle Limit 1MB";
                            return RedirectToAction("create", new { id = EmployeeMdl.id });
                        }
                    }
                }

                //check if already exist then update
                if (new DBContext().employees.ToList().FindAll(x => x.id == EmployeeMdl.id).Count > 0)
                {
                    //Update Employee
                    //EmployeeMdl.updated_at = DateTime.Now;
                    //Set image
                    //Check no image
                    if (imgFileLocation != "")
                    {
                        EmployeeMdl.photo = imgFileLocation;
                    }
                    using (var contxt = new DBContext())
                    {
                        contxt.employees.Attach(EmployeeMdl);
                        contxt.Entry(EmployeeMdl).State = EntityState.Modified;
                        contxt.SaveChanges();

                    }
                    //Set temp data Success registration message
                    TempData["message"] = Utility.SUCCESS_MESSAGE + "Employee Updated successfully";
                }
                else
                {
                    //Add new Employee
                    using (var contxt = new DBContext())
                    {
                        //Set Datetime
                       // EmployeeMdl.created_at = DateTime.Now;
                        //set comapmny id
                        EmployeeMdl.company_id = find_company.id;

                        //Set upload photo path
                        if (imgFileLocation != "")
                        {
                            EmployeeMdl.photo = imgFileLocation;
                        }

                        //Add New Employee
                        contxt.employees.Add(EmployeeMdl);
                        contxt.SaveChanges();

                    }
                    //Set temp data Success registration message
                    TempData["message"] = Utility.SUCCESS_MESSAGE + "Employee Add Successfully";
                }

                return RedirectToAction("index");
            }
            else
            {
                string errors = errorstate.errors(ModelState);
                //Set temp data for wrong credentials
                TempData["message"] = Utility.FAILED_MESSAGE + "" + errors;

                //redirect back to Registration page 
                return RedirectToAction("create", new { id = EmployeeMdl.id });
            }
        }


        public ActionResult delete(String id)
        {
            //Check if  not null
            if (id != null)
            {
                int dID = Convert.ToInt32(id);

                //Find the Employee 
                Employee dgMdl = new DBContext().employees.Find(dID);


                //Delete The Employee
                using (var contxt = new DBContext())
                {
                    contxt.Entry(dgMdl).State = EntityState.Deleted;
                    contxt.SaveChanges();
                }

                //Set temp data Success message
                TempData["message"] = Utility.SUCCESS_MESSAGE + "Employee Deleted Successfully";

            }
            else
            {
                //Set temp data Success registration message
                TempData["message"] = Utility.FAILED_MESSAGE + "Something went wrong";
            }
            return RedirectToAction("index");
        }



        public PartialViewResult employeedetails(String id)
        {
            int eID = Convert.ToInt32(id);
            Employee empMdl = new DBContext().employees.Find(eID);
            return PartialView(empMdl);
        }

    }
}