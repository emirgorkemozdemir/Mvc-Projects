using Microsoft.Ajax.Utilities;
using Mvc.Models.Classes;
using Mvc.TempClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Context = Mvc.Models.Classes.Context;

namespace Mvc.Controllers
{
    public class AccountOperationsController : Controller
    {
        public static Context con = new Context();
        public static List<User> userList = con.Set<User>().ToList();
        User loggedUser= (from k in userList where k.UserID == GlobalVariables.LoggedID select k).SingleOrDefault();
        public ActionResult AccountOperationsDetailed()
        {
            loggedUser = (from k in userList where k.UserID == GlobalVariables.LoggedID select k).SingleOrDefault();
            return View(loggedUser);
        }
        
        [HttpGet]
        public ActionResult ChangeName()
        {
            loggedUser = (from k in userList where k.UserID == GlobalVariables.LoggedID select k).SingleOrDefault();
            return View(loggedUser);
        }

        

        [HttpPost]
        public ActionResult ChangeName(User reUser)
        {
            loggedUser = (from k in userList where k.UserID == GlobalVariables.LoggedID select k).SingleOrDefault();
            loggedUser.UserName = reUser.UserName;
            var changedEntitiy = con.Entry(loggedUser);
            changedEntitiy.State = System.Data.Entity.EntityState.Modified;
            con.SaveChanges();
            return RedirectToAction("AccountOperationsDetailed",GlobalVariables.LoggedID);
        }

        [HttpGet]
        public ActionResult ChangeSurname()
        {
            loggedUser = (from k in userList where k.UserID == GlobalVariables.LoggedID select k).SingleOrDefault();
            return View(loggedUser);
        }



        [HttpPost]
        public ActionResult ChangeSurname(User reUser)
        {
            loggedUser = (from k in userList where k.UserID == GlobalVariables.LoggedID select k).SingleOrDefault();
            loggedUser.UserSurname = reUser.UserSurname;
            var changedEntitiy = con.Entry(loggedUser);
            changedEntitiy.State = System.Data.Entity.EntityState.Modified;
            con.SaveChanges();
            return RedirectToAction("AccountOperationsDetailed", GlobalVariables.LoggedID);
        }

        [HttpGet]
        public ActionResult ChangeMail()
        {
            loggedUser = (from k in userList where k.UserID == GlobalVariables.LoggedID select k).SingleOrDefault();
            return View(loggedUser);
        }



        [HttpPost]
        public ActionResult ChangeMail(User reUser)
        {
            loggedUser = (from k in userList where k.UserID == GlobalVariables.LoggedID select k).SingleOrDefault();
            loggedUser.UserMail = reUser.UserMail;
            var changedEntitiy = con.Entry(loggedUser);
            changedEntitiy.State = System.Data.Entity.EntityState.Modified;
            con.SaveChanges();
            return RedirectToAction("AccountOperationsDetailed", GlobalVariables.LoggedID);
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            loggedUser = (from k in userList where k.UserID == GlobalVariables.LoggedID select k).SingleOrDefault();
            return View(loggedUser);
        }



        [HttpPost]
        public ActionResult ChangePassword(User reUser)
        {
            loggedUser.UserPassword = reUser.UserPassword;
            var changedEntitiy = con.Entry(loggedUser);
            changedEntitiy.State = System.Data.Entity.EntityState.Modified;
            con.SaveChanges();
            return RedirectToAction("AccountOperationsDetailed", GlobalVariables.LoggedID);
        }


    }
}