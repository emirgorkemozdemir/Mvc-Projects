using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yemek_Sitesi_MVC.Models;
using Yemek_Sitesi_MVC.Models.Classes;

namespace Yemek_Sitesi_MVC.Controllers
{
    public class RecipeController : Controller
    {
        Yemek_Sitesi_VeritabanıEntities1 db = new Yemek_Sitesi_VeritabanıEntities1();
        public ActionResult ShowRecipesForNonUsers()
        {
            var listRecipes = db.YemekTablosu.ToList();
            return View(listRecipes);
        }

        public ActionResult ShowRecipesForUsers()
        {
            var listRecipes = db.YemekTablosu.ToList();
            return View(listRecipes);
        }

        public ActionResult ShowRecipeDetailsForNonUsers(int id)
        {
            var selectedRecipe = db.YemekTablosu.Find(id);
            ViewBag.photo = selectedRecipe.Resimler;
            return View(selectedRecipe);
        }

        public ActionResult ShowRecipeDetailsForUsers(int id)
        {
            var selectedRecipe = db.YemekTablosu.Find(id);
            ViewBag.photo = selectedRecipe.Resimler;
            return View(selectedRecipe);
        }

        public ActionResult TodaysRecipeForNonUser()
        {
            DateTime todayDate = DateTime.Now.Date;
            var selectedRecipe = db.GününYemeğiTablosu.Where(i => i.GYemekTarih == todayDate).SingleOrDefault();
            if (selectedRecipe==null)
            {
                return RedirectToAction("ErrorForNonUsers");
            }
            else
            {
                ViewBag.photo = selectedRecipe.GYemekResim;
                return View(selectedRecipe);
            }
         
        }

        public ActionResult TodaysRecipeForUser()
        {
            DateTime todayDate = DateTime.Now.Date;
            var selectedRecipe = db.GününYemeğiTablosu.Where(i => i.GYemekTarih == todayDate).SingleOrDefault();
            if (selectedRecipe == null)
            {
                return RedirectToAction("ErrorForUsers");
            }
            else
            {
                ViewBag.photo = selectedRecipe.GYemekResim;
                return View(selectedRecipe);
            }
           
        }

        public ActionResult AboutUsAndContactForNonUser()
        {
            var about = db.HakkımızdaTablosu.Find(1);
            ViewBag.photo = about.Resim;
            return View(about);
        }

        public ActionResult AboutUsAndContactForUser()
        {
            var about = db.HakkımızdaTablosu.Find(1);
            ViewBag.photo = about.Resim;
            return View(about);
        }

        public ActionResult GiveRecipesForNonUsers()
        {
            return View();
        }


        [HttpGet]
        public ActionResult GiveRecipesForUsers()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GiveRecipesForUsers(TarifVermeTablosu postedRecipe)
        {
            postedRecipe.TarifOnay = false;
            db.TarifVermeTablosu.Add(postedRecipe);
            db.SaveChanges();
            return RedirectToAction("MainPageForUsers");
        }

        public ActionResult ErrorForNonUsers()
        {
            return View();
        }

        public ActionResult ErrorForUsers()
        {
            return View();
        }


        public ActionResult MainPageForUsers()
        {
            return View();
        }

        public ActionResult RecipesFromUsersForNonUsers()
        {
            var listOfRecipes = db.TarifVermeTablosu.Where(i => i.TarifOnay == true).ToList();
            return View(listOfRecipes);
        }


        public ActionResult RecipesFromUsersForUsers()
        {
            var listOfRecipes = db.TarifVermeTablosu.Where(i => i.TarifOnay == true).ToList();
            return View(listOfRecipes);
        }

        public ActionResult SendComment(YemekTablosu tempComment)
        {
            YorumlarTablosu comment = new YorumlarTablosu();
            comment.Mail = tempComment.tempCommentMail;
            comment.Yorumİçerik = tempComment.tempCommentContent;
            comment.Tarih = DateTime.Now.Date;
            comment.YorumAd = GlobalVariables.loggedUser.KullanıcıAdı;
            comment.YemekID = tempComment.YemekID;

            db.YorumlarTablosu.Add(comment);
            db.SaveChanges();
            return RedirectToAction("MainPageForUsers");
        }
    }
}