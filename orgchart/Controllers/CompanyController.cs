using orgchart.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace orgchart.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        /// <summary>
        /// Get Company
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //Set  Message
            String message = "";

            //Check Tempdate error or successmessage 
            if (TempData["message"] != null)
            {
                message = TempData["message"].ToString();
            }

            //view data send message
            ViewData["message"] = message;

            //Get Current UserID
            int user_id =Convert.ToInt32(User.Identity.Name);

            //Find Company By UserID
            company cmp = new DBContext().companies.ToList().Find(x => x.user_id == user_id);

            //PASS to view
            return View(cmp);
        }

        /// <summary>
        /// Update Company
        /// </summary>
        /// <returns></returns>
        public ActionResult Store(company cmpMdl)
        {
            //Check Model state is valid or not
            if (ModelState.IsValid)
            {
                    //cmpMdl.updated_at = DateTime.Now;
                    using (var contxt = new DBContext())
                    {
                        contxt.companies.Attach(cmpMdl);
                        contxt.Entry(cmpMdl).State = EntityState.Modified;
                        contxt.SaveChanges();

                    }
                    //Set temp data Success registration message
                    TempData["message"] = Utility.SUCCESS_MESSAGE + "Company Updated successfully";

                return RedirectToAction("index");

            }
            else
            {
                string errors = errorstate.errors(ModelState);
                //Set temp data for wrong credentials
                TempData["message"] = Utility.FAILED_MESSAGE + "" + errors;

                //redirect back to Registration page 
                return RedirectToAction("index");
            }
        }
    }
}