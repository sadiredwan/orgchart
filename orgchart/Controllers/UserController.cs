using orgchart.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace orgchart.Controllers
{
    public class UserController : Controller
    {
        /// <summary>
        /// Index page 
        /// for show all designations list
        /// </summary>
        /// <returns></returns>
        // GET: User
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

            //Find all Users list into a list and bind/pass to view
            List<User> lstUsers = new DBContext().users.ToList();
            return View(lstUsers);
        }

        /// <summary>
        /// Create Page for User 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult create(String id)
        {
            //Get Current UserName
            int user_id = Convert.ToInt32(User.Identity.Name);

            //Set  Message
            String message = "";

            int uID = Convert.ToInt32(id);

            //pass model to view
            User mdlUser = new User();

            //Check if id doesnot null
            if (id != null)
            {
                //check if user already exist
                if (new DBContext().users.ToList().FindAll(x => x.id == uID).Count > 0)
                {
                    //pass model to view with user info
                    mdlUser = new DBContext().users.Find(uID);
                }
            }

            //Check Temp error or successmessage 
            if (TempData["message"] != null)
            {
                message = TempData["message"].ToString();
            }

            //view data send message
            ViewData["message"] = message;

            return View(mdlUser);
        }

        /// <summary>
        /// Store User 
        /// Create or Update
        /// </summary>
        /// <param name="userMdl"></param>
        /// <returns></returns>
        public ActionResult store(User userMdl)
        {
            //Get Current UserName
            int id = Convert.ToInt32(User.Identity.Name);

            //Check Model state is valid or not
            if (ModelState.IsValid)
            {

                //check if already exist then update
                if (new DBContext().users.ToList().FindAll(x => x.id == userMdl.id).Count > 0)
                {
                    //Update User
                    //userMdl.updated_at = DateTime.Now;
                    using (var contxt = new DBContext())
                    {
                        contxt.users.Attach(userMdl);
                        contxt.Entry(userMdl).State = EntityState.Modified;
                        contxt.SaveChanges();

                    }
                    //Set temp data Success registration message
                    TempData["message"] = Utility.SUCCESS_MESSAGE + "Designation Updated successfully";
                }
                else
                {
                    //Add new User
                    using (var contxt = new DBContext())
                    {
                        //Set Datetime
                        userMdl.created_at = DateTime.Now;
                        //Add New User
                        contxt.users.Add(userMdl);
                        contxt.SaveChanges();

                    }
                    //Set temp data Success registration message
                    TempData["message"] = Utility.SUCCESS_MESSAGE + "User Added Successfully";
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
                int uID = Convert.ToInt32(id);

                //Find the User 
                User usrMdl = new DBContext().users.Find(uID);


                //Delete The User
                using (var contxt = new DBContext())
                {
                    contxt.Entry(usrMdl).State = EntityState.Deleted;
                    contxt.SaveChanges();
                }

                //Set temp data Success registration message
                TempData["message"] = Utility.SUCCESS_MESSAGE + "User Deleted Successfully";

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