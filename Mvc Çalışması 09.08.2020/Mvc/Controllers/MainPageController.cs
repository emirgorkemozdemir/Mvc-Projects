using Mvc.Models.Classes;
using Mvc.TempClasses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Mvc.Controllers
{
    public class MainPageController : Controller
    {
        public static Context con = new Context();
        public static List<User> userList = con.Set<User>().ToList();

        [HttpGet]
        public ActionResult MainPage(User loggedUser)
        {
            return View(loggedUser);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            GlobalVariables.LoggedID = -1;
            return RedirectToAction("Login", "User");
        }

      

      
    }
}