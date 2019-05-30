using orgchart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace orgchart.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Home Page (Dashboard)
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Active or Inactive 
        /// Navigation Bar
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public ActionResult NaigationActive(String cName,String aName)
        {
            var routeValues = HttpContext.Request.RequestContext.RouteData.Values;
            string current_Cname = routeValues["controller"].ToString();
            string current_Aname = routeValues["action"].ToString();
            aName = (aName == "" ? current_Aname : aName);
            if( cName.ToLower() == current_Cname.ToLower() && aName.ToLower() == current_Aname.ToLower())
            {
                return Content("active");
            }
            else
            {
                return Content("");
            }
        }

    }
}