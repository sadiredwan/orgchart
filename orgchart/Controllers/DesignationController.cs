using orgchart.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace orgchart.Controllers
{
    public class DesignationController : Controller
    {
        /// <summary>
        /// Index page 
        /// for show all designations list
        /// </summary>
        /// <returns></returns>
        // GET: Designation
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

            //Find all Designations list into a list and bind/pass to view
            List<Designation> lstDesignations = new DBContext().designations.ToList().FindAll(x=>x.company_id==find_company.id);
            return View(lstDesignations);
        }

        /// <summary>
        /// Create Page for Designation 
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
            Designation mdlDesignation = new Designation();

            //Check if id doesnot null
            if (id!=null)
            {
                //check if designation already exist
                if(new DBContext().designations.ToList().FindAll(x=>x.id==dID).Count>0)
                {
                    //pass model to view with designation info
                    mdlDesignation = new DBContext().designations.Find(dID);
                }
            }

            //Check Temp error or successmessage 
            if (TempData["message"] != null)
            {
                message = TempData["message"].ToString();
            }
          

            //view data send message
            ViewData["message"] = message;

            return View(mdlDesignation);
        }

        /// <summary>
        /// Store Desiognation 
        /// Create or Update
        /// </summary>
        /// <param name="designationMdl"></param>
        /// <returns></returns>
        public ActionResult store(Designation designationMdl)
        {
            //Get Current UserName
            int id = Convert.ToInt32(User.Identity.Name);
            //Current Compamy
            company find_company = new DBContext().companies.ToList().Find(x => x.user_id == id);

            //Check Model state is valid or not
            if (ModelState.IsValid)
            {

                //check if already exist then update
                if (new DBContext().designations.ToList().FindAll(x => x.id == designationMdl.id).Count > 0)
                {
                    //Update Designation
                    //designationMdl.updated_at = DateTime.Now;
                    using (var contxt = new DBContext())
                    {
                        contxt.designations.Attach(designationMdl);
                        contxt.Entry(designationMdl).State = EntityState.Modified;
                        contxt.SaveChanges();

                    }
                    //Set temp data Success registration message
                    TempData["message"] = Utility.SUCCESS_MESSAGE + "Designation Updated successfully";
                }
                else
                {
                    //Add new Designation
                    using (var contxt = new DBContext())
                    {
                        //Set Datetime
                        designationMdl.created_at = DateTime.Now;
                        //set comapmny id
                        designationMdl.company_id = find_company.id;

                        //Add New Designation
                        contxt.designations.Add(designationMdl);
                        contxt.SaveChanges();

                    }
                    //Set temp data Success registration message
                    TempData["message"] = Utility.SUCCESS_MESSAGE + "Designation Added Successfully";
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

                //Find the designation 
                Designation dgMdl = new DBContext().designations.Find(dID);


                //Delete The Designation
                using (var contxt = new DBContext())
                {
                    contxt.Entry(dgMdl).State = EntityState.Deleted;
                    contxt.SaveChanges();
                }

                //Set temp data Success registration message
                TempData["message"] = Utility.SUCCESS_MESSAGE + "Designation Deleted Successfully";

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