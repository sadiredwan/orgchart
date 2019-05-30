using orgchart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace orgchart.Controllers
{
    /// <summary>
    /// AccountConyroller for Login and Registration 
    /// </summary>
    public class AccountController : Controller
    {
        [AllowAnonymous]
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Login Page
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login()
        {
            //Set  Message
            String message = "";

            //Check Tempdate error or successmessage 
            if(TempData["message"]!=null)
            {
                message = TempData["message"].ToString();
            }
            //pass model to view
            LoginUser modelusr = new LoginUser();

            //view data send message
            ViewData["message"] = message;

            return View(modelusr);
        }

        /// <summary>
        /// Check Login User Autinicated
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult LoginStore(LoginUser user)
        {
            //Check Model State Validation 
            //Like: Required Filed validate
            if (ModelState.IsValid)
            {
                //password  encode
                String password = Utility.Encode(user.password);
                //check user autinaction
                if (new DBContext().users.ToList().FindAll(x => x.email == user.email && x.password == password).Count > 0)
                {
                    //Find Loggedin UserID
                    String user_id = "" + new DBContext().users.ToList().FindAll(x => x.email == user.email && x.password == password).FirstOrDefault().id;
                    //Set authication id
                    FormsAuthentication.SetAuthCookie(user_id, false);
                    //Redirect to home page
                    return RedirectToAction("index", "home");
                }
                else
                {
                    //Set temp data for wrong credentials
                    TempData["message"] = Utility.FAILED_MESSAGE + " Wrong Credentials";

                    //redirect back to login page 
                    return RedirectToAction("login");
                }
            }
            else
            {
                string errors = errorstate.errors(ModelState);
                //Set temp data for wrong credentials
                TempData["message"] = errors;

                //redirect back to login page 
                return RedirectToAction("login");
            }


        }

        /// <summary>
        /// Registration Page
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Register()
        {
            //Set  Message
            String message = "";

            //Check Tempdate error or successmessage 
            if (TempData["reg_message"] != null)
            {
                message = TempData["reg_message"].ToString();
            }
            //pass model to view
            User modelusr = new User();

            //view data send message
            ViewData["message"] = message;
            ViewBag.Title = this.ControllerContext.RouteData.Values["action"].ToString(); 

            return View(modelusr);
        }

        /// <summary>
        /// Registration Store
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult RegisterStore(User regUser)
        {
            //Check Model state is valid or not
            if(ModelState.IsValid)
            {
                String md5password = Utility.Encode(regUser.password);

                using (var contxt = new DBContext())
                {
                    //Set Datetime
                    regUser.created_at = DateTime.Now;
                    regUser.password = md5password;
                    regUser.confirm_password = md5password;
                    //Add New User
                    contxt.users.Add(regUser);
                    contxt.SaveChanges();

                    //Get Current User ID
                    int user_id = regUser.id;

                    //Save into Company Table
                    company cmpMdl = new company();
                    cmpMdl.name = regUser.Company_Name;
                    cmpMdl.user_id = regUser.id;
                    cmpMdl.created_at = DateTime.Now;
                    contxt.companies.Add(cmpMdl);
                    contxt.SaveChanges();
                }
                //Set temp data Success registration message
                TempData["reg_message"] = Utility.SUCCESS_MESSAGE + " You have been Registered Successfully ,Please login";
                return RedirectToAction("Register");
            }
            else
            {
                string errors = errorstate.errors(ModelState);
                //Set temp data for wrong credentials
                TempData["reg_message"] = Utility.FAILED_MESSAGE+""+errors;

                //redirect back to Registration page 
                return RedirectToAction("Register");
            }
            
        }

        /// <summary>
        /// Signout from Application
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOff()
        {
            //Signout from authication system
            FormsAuthentication.SignOut();

            //redirect to login page
            return RedirectToAction("login");
        }
    }
}