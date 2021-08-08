using Mvc_Deneme_ArabaSitesi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc_Deneme_ArabaSitesi.Controllers
{
    public class UserController : Controller
    {
        ArabaYoutubeEntities1 db = new ArabaYoutubeEntities1();

        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(TempLoginClass tempLogin)
        {
            var hashedPass = Sha256Converter.ComputeSha256Hash(tempLogin.EnteredPass);
          var selectedUser=  db.TableUser.Where(i => i.UserMail == tempLogin.EnteredMail && i.UserPassword == hashedPass).SingleOrDefault();
            if (selectedUser==null)
            {
                Response.Write("Mail or password is wrong");
                return View();
            }
            else
            {
                Response.Write("Login succesfull");
                return RedirectToAction("AdminMainPage","Admin");
            }
            
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(TableUser newUser)
        {
            var existingUser = db.TableUser.Where(i => i.UserMail == newUser.UserMail).SingleOrDefault();
            if (ModelState.IsValid)
            {
                if (existingUser!=null)
                {
                    Response.Write("<script>alert('The e-mail is taken , please enter another e-mail adress');</script>");
                    return View();
                }
                else
                {
                    var hashedPass = Sha256Converter.ComputeSha256Hash(newUser.UserPassword);
                    newUser.UserPassword = hashedPass;
                    db.TableUser.Add(newUser);
                    db.SaveChanges();
                    Response.Write("<script>alert('Register Successful');</script>");
                    return RedirectToAction("Login");
                }
            }
            else
            {
                return View();
            }
        }
    }
}