using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Yemek_Sitesi_MVC.Models;
using Yemek_Sitesi_MVC.Models.Classes;


namespace Yemek_Sitesi_MVC.Controllers
{
    public class UserController : Controller
    {
        Yemek_Sitesi_VeritabanıEntities1 db = new Yemek_Sitesi_VeritabanıEntities1();
    
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(YöneticiTablosu tempUser)
        {
            if (ModelState.IsValid)
            {
                var shaPass = Sha265Converter.ComputeSha256Hash(tempUser.Şifre);

                tempUser.Şifre = shaPass;

                db.YöneticiTablosu.Add(tempUser);

                db.SaveChanges();
            }
         

            return View();
        }

        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(YöneticiTablosu tempUser)
        {
            if (ModelState.IsValid)
            {
                var hashedPas = Sha265Converter.ComputeSha256Hash(tempUser.Şifre);
                var loggingUser = db.YöneticiTablosu.Where(i => i.KullanıcıAdı == tempUser.KullanıcıAdı && i.Şifre == hashedPas).SingleOrDefault();

                if (loggingUser == null)
                {
                    Response.Write("Username or password is wrong !");

                }
                else if (loggingUser.KullanıcıAdı=="Admin")
                {
                    Response.Write("Login success");
                    GlobalVariables.loggedUser = loggingUser;
                    return RedirectToAction("AdminListRecipes", "Admin");
                }
                else
                {
                    Response.Write("Login success");
                    GlobalVariables.loggedUser = loggingUser;
                    return RedirectToAction("MainPageForUsers", "Recipe");

                }

               
            }
            return View();
        }

        public ActionResult LogOut()
        {

            FormsAuthentication.SignOut();
            Session.Abandon();
            GlobalVariables.loggedUser = null;
            return RedirectToAction("SignIn");
        }

        [HttpGet]
        public ActionResult UserSettings()
        {
            var selectedUser = GlobalVariables.loggedUser;
            return View(selectedUser);
        }

        [HttpPost]
        public ActionResult UserSettings(YöneticiTablosu tempUser)
        {
        
            if (Sha265Converter.ComputeSha256Hash( tempUser.oldPassword)==GlobalVariables.loggedUser.Şifre)
            {
                var editingUser = db.YöneticiTablosu.Where(i => i.KullanıcıAdı == GlobalVariables.loggedUser.KullanıcıAdı).SingleOrDefault();
                editingUser.Şifre =Sha265Converter.ComputeSha256Hash( tempUser.NewPassword);
                db.SaveChanges();
                return RedirectToAction("MainPageForUsers", "Recipe");
            }
            else
            {
                Response.Write("Eski şifre yanlış !");
                return RedirectToAction("UserSettings");
            }
        }
    }
}