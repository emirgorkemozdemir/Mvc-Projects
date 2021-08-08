using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Mvc_Kütüphane.Models;
namespace Mvc_Kütüphane.Controllers
{
    public class UserController : Controller
    {
        KütüphaneProjesiEntities db = new KütüphaneProjesiEntities();

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(KütüphaneKullanıcıTablosu2 newUser)
        {
            if (ModelState.IsValid)
            {
                var existingUser = db.KütüphaneKullanıcıTablosu2.Where(i => i.GirişBilgisi == newUser.GirişBilgisi).SingleOrDefault();
                if (existingUser==null)
                {
                   
                    if (newUser.Şifre.Length<=8)
                    {
                        Response.Write("<script> alert('Şifre uzunluğu 8den kısa olamaz !'); </script>");
                      
                    }
                    else if (PasswordControl.GetLowerScore(newUser.Şifre)<1)
                    {
                        Response.Write("<script> alert('Şifrenizde en az bir adet küçük harf bulunmalıdır !'); </script>");
                    }
                    else if (PasswordControl.GetUpperScore(newUser.Şifre) < 1)
                    {
                        Response.Write("<script> alert('Şifrenizde en az bir adet büyük harf bulunmalıdır !'); </script>");
                    }
                    else if (PasswordControl.GetDigitScore(newUser.Şifre) < 1)
                    {
                        Response.Write("<script> alert('Şifrenizde en az bir adet sayı bulunmalıdır !'); </script>");
                    }
                    else
                    {
                        var newPass = Sha256Converter.ComputeSha256Hash(newUser.Şifre);
                        newUser.Şifre = newPass;
                        newUser.Rank = 3;
                        db.KütüphaneKullanıcıTablosu2.Add(newUser);
                        db.SaveChanges();
                        Response.Write("<script> alert('Kayıt Başarılı'); </script>");
                    }
                 
                   
                }
                else
                {
                    Response.Write("<script> alert('Girdiğiniz Mail Adresi Kullanılmaktadır !'); </script>");
                }
            
            }
            else
            {

            }

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(KütüphaneKullanıcıTablosu2 loggingUser)
        {
          
                var newPass = Sha256Converter.ComputeSha256Hash(loggingUser.Şifre);
                var loggedUser = db.KütüphaneKullanıcıTablosu2.Where(i => i.GirişBilgisi == loggingUser.GirişBilgisi && i.Şifre == newPass).SingleOrDefault();
                if (loggedUser != null)
                {
                    LoggedUserInfo.Mail = loggedUser.GirişBilgisi;
                    LoggedUserInfo.Pass = loggedUser.Şifre;
                    LoggedUserInfo.Rank = loggedUser.Rank;
                    return RedirectToAction("CheckUser", "User");
                }
                else
                {
                    Response.Write("<script> alert('Kullanıcı adı veya şifre hatalı !'); </script>");
                }
           
         
            return View();
        }

        public ActionResult CheckUser()
        {
            if (LoggedUserInfo.Rank==3)
            {
                return RedirectToAction("MainPage", "Books");
            }
            else if (LoggedUserInfo.Rank==1)
            {
                return RedirectToAction("AdminMainPage", "Admin");
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            LoggedUserInfo.Mail = "";
            LoggedUserInfo.Pass = "";
            LoggedUserInfo.Rank = -1;
            return RedirectToAction("Login", "User");
        }
    }
}