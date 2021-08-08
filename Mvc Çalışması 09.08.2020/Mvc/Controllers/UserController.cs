using Mvc.Models.Classes;
using Mvc.TempClasses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Controllers
{
    public class UserController : Controller
    {
        public static Context con = new Context();
        public static List<User> userList = con.Set<User>().ToList();
        string validationError = "";

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User tempUser)
        {

            var addEntity = con.Entry(tempUser);
            addEntity.State = EntityState.Added;
            con.SaveChanges();


            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.VE = validationError;
            return View();
        }



        [HttpPost]
        public ActionResult Login(User loggingUser)
        {

            if (selectUser(loggingUser.UserMail) != null)
            {
                User tempUser = selectUser(loggingUser.UserMail);
                if (tempUser.UserPassword == loggingUser.UserPassword)
                {
                    User loggedUser = tempUser;
                    tempUser.Logged = true;
                    GlobalVariables.LoggedID = tempUser.UserID;

                    return View("~/Views/MainPage/MainPageUser.cshtml",loggedUser);
                }
                else
                {
                    validationError = "Password is not true !";
                    ViewBag.VE = validationError;
                    return View();
                }
            }
            else
            {
                validationError = "There is no registered account with this mail adress !";
                ViewBag.VE = validationError;
                return View();
            }

        }

        public User selectUser(string tempMail)
        {
            return (from k in userList where k.UserMail == tempMail select k).SingleOrDefault();
        }

    }
}